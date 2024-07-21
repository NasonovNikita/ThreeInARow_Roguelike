using System;
using System.Collections.Generic;
using Audio;
using Battle.UI;
using Battle.Units;
using Core.Saves;
using UI;
using UI.MessageWindows;
using UnityEngine;
using UnityEngine.Serialization;

namespace Battle
{
    public class SceneManager : MonoBehaviour
    {
        public static EnemyGroup enemyGroup;

        [SerializeField] private Canvas mainCanvas;
        [SerializeField] private EnemyPlacer placer;

        [SerializeField] private BattleLose loseMessage;

        [FormerlySerializedAs("winMessage")] [SerializeField]
        private BattleWinWindow winMessageWindow;

        private readonly List<Enemy> enemiesWithNulls = new();

        public void Awake()
        {
            GameSave.Save();

            InitEnemies();
        }

        public void Start()
        {
            AudioManager.Instance.StopAll();
            AudioManager.Instance.Play(AudioEnum.Battle);

            TurnLabel.Instance.SetPlayerTurn();

            BattleFlowManager.Instance.enemiesWithNulls = enemiesWithNulls;
            BattleFlowManager.Instance.TurnOn();
            PickerManager.Instance.PickNextPossible();

            Player.Instance.LateLoad();

            BattleFlowManager.Instance.OnBattleWin += WinBattle;
            BattleFlowManager.Instance.OnBattleLose += LoseBattle;
            BattleFlowManager.Instance.OnEnemiesShuffle += PlaceEnemies;
            BattleFlowManager.Instance.OnEnemiesTurnStart += TurnLabel.Instance.SetEnemyTurn;
            BattleFlowManager.Instance.OnPlayerTurnStart += TurnLabel.Instance.SetPlayerTurn;

            OnSceneFullyLoaded?.Invoke();
        }

        public void OnDestroy()
        {
            OnSceneLeave?.Invoke();
        }

        public static event Action OnSceneFullyLoaded;
        public static event Action OnSceneLeave;

        private void InitEnemies()
        {
            foreach (Enemy enemy in enemyGroup.Enemies)
            {
                if (enemy == null)
                {
                    enemiesWithNulls.Add(null);
                    continue;
                }

                enemiesWithNulls.Add(LoadEnemy(enemy));
            }

            PlaceEnemies();
        }

        private Enemy LoadEnemy(Enemy enemy)
        {
            return Instantiate(enemy, mainCanvas.transform, false);
        }

        private void PlaceEnemies()
        {
            EnemyPlacer.Instance.Place(enemiesWithNulls);
        }

        private void LoseBattle()
        {
            Instantiate(loseMessage, UICanvas.Instance.transform);
        }

        private void WinBattle()
        {
            winMessageWindow.Create(enemyGroup.Reward, UICanvas.Instance.transform);
        }
    }
}