using System;
using System.Collections.Generic;
using Audio;
using Battle.Grid;
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
        public static EnemyGroup EnemyGroup;

        [SerializeField] private Canvas mainCanvas;
        [SerializeField] private EnemyPlacer placer;

        [SerializeField] private BattleLose loseMessage;

        [FormerlySerializedAs("winMessage")] [SerializeField]
        private BattleWinWindow winMessageWindow;

        private readonly List<Enemy> _enemiesWithNulls = new();

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

            BattleFlowManager.Instance.EnemiesWithNulls = _enemiesWithNulls;
            BattleFlowManager.Instance.Init();
            PickerManager.Instance.PickNextPossible();

            if (EnemyGroup.isBoss)
            {
                ChangeGridSize(2);

                BattleFlowManager.Instance.OnBattleEnd += () => ChangeGridSize(-2);
            }
            Grid.Grid.Instance.Init();
            GridResizer.Instance.Resize();
            GridGenerator.Instance.Init();
            

            Player.Instance.Init();

            BattleFlowManager.Instance.OnBattleWin += WinBattle;
            BattleFlowManager.Instance.OnBattleLose += LoseBattle;
            BattleFlowManager.Instance.OnEnemiesShuffle += PlaceEnemies;
            BattleFlowManager.Instance.OnEnemiesTurnStart +=
                TurnLabel.Instance.SetEnemyTurn;
            BattleFlowManager.Instance.OnPlayerTurnStart +=
                TurnLabel.Instance.SetPlayerTurn;

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
            foreach (var enemy in EnemyGroup.Enemies)
            {
                if (enemy == null)
                {
                    _enemiesWithNulls.Add(null);
                    continue;
                }

                _enemiesWithNulls.Add(LoadEnemy(enemy));
            }

            PlaceEnemies();
        }

        private Enemy LoadEnemy(Enemy enemy) =>
            Instantiate(enemy, mainCanvas.transform, false);

        private void ChangeGridSize(int dSize)
        {
            Grid.Grid.Instance.sizeX += dSize;
            Grid.Grid.Instance.sizeY += dSize;
        }

        private void PlaceEnemies()
        {
            EnemyPlacer.Instance.Place(_enemiesWithNulls);
        }

        private void LoseBattle()
        {
            Instantiate(loseMessage, UICanvas.Instance.transform);
        }

        private void WinBattle()
        {
            winMessageWindow.Create(EnemyGroup.Reward, UICanvas.Instance.transform);
        }
    }
}