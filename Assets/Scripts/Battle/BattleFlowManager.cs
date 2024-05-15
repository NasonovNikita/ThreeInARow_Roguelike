using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle.Units;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Battle
{
    public class BattleFlowManager : MonoBehaviour
    {
        public static BattleFlowManager Instance { get; private set; }

        public const Unit NoTurningUnit = null;

        [NonSerialized]
        public List<Enemy> enemiesWithNulls = new();
        public List<Enemy> EnemiesWithoutNulls => enemiesWithNulls.Where(enemy => enemy != null).ToList();

        public Unit CurrentlyTurningUnit { get; private set; }

        public event Action OnBattleStart;
        public event Action OnCycleEnd;
        public event Action OnBattleEnd;
        public event Action OnBattleWin;
        public event Action OnBattleLose;
        public event Action OnEnemiesShuffle;
        public event Action OnEnemiesTurnStart;
        public event Action OnPlayerTurnStart;

        public void Awake() => 
            Instance = this;

        public void TurnOn()
        {
            InitEnemies();
            PlayerTurn();

            Player.Instance.OnDied += OnPlayerDeath;
            
            OnBattleStart?.Invoke();
        }

        private void InitEnemies()
        {
            foreach (Enemy enemy in EnemiesWithoutNulls)
            {
                enemy.target = Player.Instance;
                enemy.OnDied += OnEnemyDeath;

                if (!enemy.TryGetComponent(out ManaByTimeIncreaser component)) continue;
                
                component.StartIncreasing();
            }
        }

        private void OnEnemyDeath()
        {
            if (EnemiesWithoutNulls.Any(enemy => !enemy.Dead)) return;

            BattleEnd();
            OnBattleWin?.Invoke();
        }

        private void OnPlayerDeath()
        {
            BattleEnd();
            OnBattleLose?.Invoke();
        }

        public void StartEnemiesTurn() => StartCoroutine(EnemiesAct());

        public void ShuffleEnemies()
        {
            enemiesWithNulls = enemiesWithNulls.OrderBy(_ => Random.Range(0, 100000)).ToList();
            OnEnemiesShuffle?.Invoke();
        }

        private IEnumerator EnemiesAct()
        {
            OnEnemiesTurnStart?.Invoke();
            
            foreach (Enemy enemy in EnemiesWithoutNulls)
            {
                CurrentlyTurningUnit = enemy;
                yield return StartCoroutine(enemy.Turn());
            }
            OnCycleEnd?.Invoke();
            PlayerTurn();
        }

        private void PlayerTurn()
        {
            OnPlayerTurnStart?.Invoke();
            CurrentlyTurningUnit = Player.Instance;
            Player.Instance.StartTurn();
        }

        private void BattleEnd()
        {
            CurrentlyTurningUnit = NoTurningUnit;
            OnBattleEnd?.Invoke();
        }
    }
}