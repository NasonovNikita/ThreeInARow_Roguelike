using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
        public static BattleFlowManager Instance { get; private set; }
        
        [NonSerialized] public List<Enemy> EnemiesWithNulls = new();

        /// <summary>
        ///     <b>Put processes</b>, that must be ended
        ///     before manager goes to next step (e.g. next unit's turn), <b>here</b>.
        /// </summary>

        public List<Enemy> EnemiesWithoutNulls =>
            EnemiesWithNulls.Where(enemy => enemy != null).ToList();

        public Unit CurrentlyTurningUnit { get; private set; }

        public bool AllowedToUseGrid => _states.Peek() == BattleState.PlayerTurn;
        public bool AllowedToUseSpells => _states.Peek() == BattleState.PlayerTurn;
        
        private const Unit NoTurningUnit = null;
        
        private readonly Stack<BattleState> _states = new();

        private readonly List<SmartCoroutine> _processes = new();


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
            _states.Push(BattleState.Entry);
            
            InitEnemies();

            Player.Instance.OnDied += OnPlayerDeath;

            LaunchPlayerTurn();
            OnBattleStart?.Invoke();
        }

        public void AddProcess(SmartCoroutine coroutine)
        {
            if (coroutine is null) return;  // In case when coroutine is launched through TryRestart (may not restart with null)
            _states.Push(BattleState.Processing);
            if (_processes.Contains(coroutine)) return;  // In case the same coroutine is being restarted
            _processes.Add(coroutine);
            coroutine.Last.OnFinished += () => _states.Pop();
        }

        public void StopPlayerTurn()
        {
            if (_states.Peek() != BattleState.PlayerTurn)
            {
                throw new WarningException(
                    "Asked to stop Player's turn immediately when it isn't his turn or some processes are to be made..");
            }
            
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
            _states.Push(BattleState.PlayerTurn);
            Player.Instance.RefillMoves();
            CurrentlyTurningUnit = Player.Instance;

            OnPlayerTurnStart?.Invoke();

            Player.Instance.StartTurn();
            
            // Be careful with changing. There can't be any WaitUntil waiting only for turnCount becoming 0 (see. Hourglass cell).
            new SmartCoroutine(this,
                () => new WaitUntil(() =>
                    Player.Instance.CurrentMovesCount == 0 && _states.Peek() == BattleState.PlayerTurn),
                onEnd: () =>
                {
                    _states.Pop();
                    StartCoroutine(EnemiesTurn());
                }).Start();
        }

        private IEnumerator EnemiesTurn()
        {
            _states.Push(BattleState.EnemyTurn);
            OnEnemiesTurnStart?.Invoke();

            foreach (var enemy in EnemiesWithoutNulls)
            {
                CurrentlyTurningUnit = enemy;
                yield return StartCoroutine(enemy.Turn());
            }

            OnCycleEnd?.Invoke();
            CyclesDone++;

            yield return new WaitUntil(() => _states.Peek() != BattleState.Processing);
            _states.Pop();
            LaunchPlayerTurn();
        }

        private void BattleEnd()
        {
            _states.Push(BattleState.End);
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
    }
}