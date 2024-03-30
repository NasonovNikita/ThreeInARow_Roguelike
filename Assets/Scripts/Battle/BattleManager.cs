using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Audio;
using Battle.Units;
using UI.Battle;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Battle
{
    public class BattleManager : MonoBehaviour
    {
        public static EnemyGroup enemyGroup;
        
        [SerializeField] private EnemyPlacer placer;

        [SerializeField] private Canvas mainCanvas;
        [SerializeField] private Canvas uiCanvas;
        [SerializeField] private TurnLabel turnHUD;
        
        [SerializeField] private UI.MessageWindows.BattleLose loseMessage;
        [SerializeField] private UI.MessageWindows.BattleWin winMessage;
        
        [SerializeField] private Player player;

        public List<Enemy> Enemies { get; private set; } = new ();

        public Unit CurrentlyTurningUnit { get; private set; }

        public static Action onBattleStart;
        public Action onTurnEnd;
        public Action onBattleEnd;
        
        public void Awake()
        {
            AudioManager.instance.StopAll();
            Core.Saves.GameSave.Save();
            AudioManager.instance.Play(AudioEnum.Battle);
            
            InitEnemies();
            onBattleStart?.Invoke();
            PlayerTurn();
        }
        
        public void OnPlayerDeath()
        {
            BattleEnd();
            Instantiate(loseMessage, uiCanvas.transform);
        }

        public void OnEnemyDeath()
        {
            if (Enemies.Any(enemy => enemy != null)) return;

            BattleEnd();
            Instantiate(winMessage, uiCanvas.transform);
        }

        public void StartEnemiesTurn() => StartCoroutine(EnemiesAct());

        public void ShuffleEnemies()
        {
            Enemies = Enemies.OrderBy(_ => Random.Range(0, 100000)).ToList();
            placer.Place(Enemies);
        }

        private IEnumerator EnemiesAct()
        {
            foreach (var enemy in Enemies)
            {
                CurrentlyTurningUnit = enemy;
                yield return StartCoroutine(enemy.Turn());
            }
            onTurnEnd?.Invoke();
            PlayerTurn();
        }

        private void PlayerTurn()
        {
            turnHUD.SetPlayerTurn();
            CurrentlyTurningUnit = player;
            player.StartTurn();
        }

        private void BattleEnd()
        {
            CurrentlyTurningUnit = null;
            onBattleEnd?.Invoke();
        }

        private void InitEnemies()
        {
            foreach (Enemy enemy in enemyGroup.GetEnemies())
            {
                Enemies.Add(LoadEnemy(enemy));
            }
            
            PlaceEnemies();
        }

        private Enemy LoadEnemy(Enemy enemy)
        {
            if (enemy == null) return null;
            enemy = Instantiate(enemy, mainCanvas.transform, false);
            enemy.target = player;
            
            return enemy;
        }

        private void PlaceEnemies() => placer.Place(Enemies);
    }
}