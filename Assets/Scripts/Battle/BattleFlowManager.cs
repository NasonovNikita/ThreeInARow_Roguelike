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

        private const Unit NoTurningUnit = null;

        [NonSerialized] public List<Enemy> enemiesWithNulls = new();

        public List<Enemy> EnemiesWithoutNulls =>
            enemiesWithNulls.Where(enemy => enemy != null).ToList();

        public Unit CurrentlyTurningUnit { get; private set; }

        public List<Func<bool>> endedProcesses;

        public event Action OnBattleStart;
        public event Action OnCycleEnd;
        public event Action OnBattleEnd;
        public event Action OnBattleWin;
        public event Action OnBattleLose;
        public event Action OnEnemiesShuffle;
        public event Action OnEnemiesTurnStart;
        public event Action OnPlayerTurnStart;

        public void Awake()
        {
            Instance = this;
        }

        public void OnDestroy()
        {
            BattleEnd();
        }

        public void TurnOn()
        {
            InitEnemies();
            PlayerTurn();

            Player.Instance.OnDied += OnPlayerDeath;

            OnBattleStart?.Invoke();
        }

        public void StopPlayerTurn()
        {
            Player.Instance.WasteAllMoves();
            CurrentlyTurningUnit = NoTurningUnit;
        }

        public void ShuffleEnemies()
        {
            enemiesWithNulls = enemiesWithNulls.OrderBy(_ => Random.Range(0, 100000)).ToList();
            OnEnemiesShuffle?.Invoke();
        }

        private void PlayerTurn()
        {
            endedProcesses = new List<Func<bool>>();
            
            Player.Instance.RefillMoves();
            CurrentlyTurningUnit = Player.Instance;
            
            OnPlayerTurnStart?.Invoke();
            
            Player.Instance.StartTurn();

            StartCoroutine(WaitForProcessesToEnd(() => StartCoroutine(EnemiesTurn())));
        }

        private IEnumerator EnemiesTurn()
        {
            yield return StartCoroutine(WaitForProcessesToEnd());
            OnEnemiesTurnStart?.Invoke();

            foreach (Enemy enemy in EnemiesWithoutNulls)
            {
                CurrentlyTurningUnit = enemy;
                yield return StartCoroutine(enemy.Turn());
                yield return StartCoroutine(WaitForProcessesToEnd());
            }

            OnCycleEnd?.Invoke();
            yield return StartCoroutine(WaitForProcessesToEnd());
            PlayerTurn();
        }

        private void BattleEnd()
        {
            CurrentlyTurningUnit = NoTurningUnit;
            OnBattleEnd?.Invoke();
        }

        private void InitEnemies()
        {
            foreach (Enemy enemy in EnemiesWithoutNulls)
            {
                enemy.target = Player.Instance;
                enemy.OnDied += OnEnemyDeath;

                if (!enemy.TryGetComponent(out ManaByTimeIncreaser increaser)) continue;

                increaser.StartIncreasing();
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

        private IEnumerator WaitForProcessesToEnd(Action onEnd = null)
        {
            yield return new WaitUntil(() => endedProcesses.All(f => f.Invoke()));
            
            onEnd?.Invoke();
        }
    }
}