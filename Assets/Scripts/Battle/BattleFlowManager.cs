using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle.Units;
using Other;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Battle
{
    /// <summary>
    ///     Main battle manager.<br/>
    ///     Controls which unit is turning,
    /// </summary>
    public class BattleFlowManager : MonoBehaviour
    {
        private const Unit NoTurningUnit = null;

        private SmartCoroutine _mainFlowCoroutine;

        [NonSerialized] public List<Enemy> EnemiesWithNulls = new();

        /// <summary>
        ///     <b>Put processes</b>, that must be ended
        ///     before manager goes to next step (e.g. next unit's turn), <b>here</b>.
        /// </summary>
        public List<SmartCoroutine> Processes = new();

        public static BattleFlowManager Instance { get; private set; }

        public List<Enemy> EnemiesWithoutNulls =>
            EnemiesWithNulls.Where(enemy => enemy != null).ToList();

        public Unit CurrentlyTurningUnit { get; private set; }

        // Was used before. Will be kept to possibly use in some mechanics.
        // ReSharper disable once MemberCanBePrivate.Global
        public int CyclesDone { get; private set; }

        public void Awake()
        {
            Instance = this;
        }

        public void OnDestroy()
        {
            BattleEnd();
        }

        public event Action OnBattleStart;

        /// <summary>
        ///     One cycle is when Player and Enemies made their turns.
        /// </summary>
        public event Action OnCycleEnd;

        public event Action OnBattleEnd;
        public event Action OnBattleWin;
        public event Action OnBattleLose;
        public event Action OnEnemiesShuffle;
        public event Action OnEnemiesTurnStart;
        public event Action OnPlayerTurnStart;

        /// <summary>
        ///     Not only inits units but also start the battle itself.
        /// </summary>
        public void Init()
        {
            InitEnemies();

            Player.Instance.OnDied += OnPlayerDeath;

            _mainFlowCoroutine = new SmartCoroutine(
                this,
                SmartCoroutine.StackCoroutines(
                    this,
                    new Func<IEnumerator>[]
                    {
                        WaitForProcessesToEnd,
                        EnemiesTurn,
                        WaitForProcessesToEnd
                    }
                ),
                () =>
                {
                    Processes = new List<SmartCoroutine>(); // Initiate new Cycle
                    LaunchPlayerTurn();
                });
            _mainFlowCoroutine.Last.Next = _mainFlowCoroutine; // Cycle

            _mainFlowCoroutine.Start();
            OnBattleStart?.Invoke();
        }

        public void StopPlayerTurn()
        {
            Player.Instance.WasteAllMoves();
            CurrentlyTurningUnit = NoTurningUnit;
        }

        /// <summary>
        ///     Enemies change their order in collection.<br/>
        ///     This affects game rule with front and backing row.
        /// </summary>
        /// <seealso cref="OnEnemiesShuffle"/>
        public void ShuffleEnemies()
        {
            EnemiesWithNulls =
                EnemiesWithNulls.OrderBy(_ => Random.Range(0, 100000)).ToList();
            OnEnemiesShuffle?.Invoke();
        }

        private void LaunchPlayerTurn()
        {
            Player.Instance.RefillMoves();
            CurrentlyTurningUnit = Player.Instance;

            OnPlayerTurnStart?.Invoke();

            Player.Instance.StartTurn();
        }

        private IEnumerator EnemiesTurn()
        {
            OnEnemiesTurnStart?.Invoke();

            foreach (var enemy in EnemiesWithoutNulls)
            {
                CurrentlyTurningUnit = enemy;
                yield return StartCoroutine(enemy.Turn());
                yield return StartCoroutine(WaitForProcessesToEnd());
            }

            OnCycleEnd?.Invoke();
            CyclesDone++;
        }

        private void BattleEnd()
        {
            CurrentlyTurningUnit = NoTurningUnit;
            OnBattleEnd?.Invoke();
        }

        private void InitEnemies()
        {
            foreach (var enemy in EnemiesWithoutNulls)
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

        private IEnumerator WaitForProcessesToEnd()
        {
            yield return new WaitUntil(() =>
                Processes.All(coroutine => coroutine.Finished));
        }
    }
}