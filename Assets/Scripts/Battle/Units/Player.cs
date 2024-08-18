using System;
using System.Collections.Generic;
using System.Linq;
using Audio;
using Battle.Modifiers;
using Battle.Spells;
using Battle.Units.Statuses;
using UnityEngine;

namespace Battle.Units
{
    /// <summary>
    ///     Has moves (does multiple actions in one turn).
    ///     Is controlled by <i>Player</i>.
    /// </summary>
    [Serializable]
    public class Player : Unit
    {
        public static PlayerData Data;
        [SerializeField] private int maxMoves;
        public int CurrentMovesCount { get; private set; }

        public static Player Instance { get; private set; }

        public override List<Unit> Enemies =>
            new(BattleFlowManager.Instance.EnemiesWithoutNulls);

        public override void Awake()
        {
            Instance = this;

            hp.OnValueChanged += _ => AudioManager.Instance.Play(AudioEnum.PlayerHit);

            RefillMoves();
            Load();
            base.Awake();
        }

        public void OnDestroy()
        {
            Save();
        }

        public event Action OnMovesCountChanged;

        public void RefillMoves()
        {
            CurrentMovesCount = maxMoves;
            OnMovesCountChanged?.Invoke();
        }

        public void WasteAllMoves()
        {
            CurrentMovesCount = 0;
            OnMovesCountChanged?.Invoke();
        }

        public void AddMove()
        {
            CurrentMovesCount++;
        }

        public void AddMoves(int count)
        {
            CurrentMovesCount += count;
        }

        public void WasteMove()
        {
            CurrentMovesCount -= 1;
            OnMovesCountChanged?.Invoke();
        }

        public void StartTurn()
        {
            BattleFlowManager.Instance.EndedProcesses.Add(() => CurrentMovesCount == 0);

            if (Statuses.List.Exists(mod => mod is Stun { EndedWork: false }))
                WasteAllMoves();
        }

        private void Load()
        {
            hp = Data.hp;
            mana = Data.mana;
            damage = Data.damage;

            spells = new List<Spell>(Data.spells);
            Statuses = new ModifierList(Data.statuses);
        }

        public void Init()
        {
            hp.Init();
            mana.Init();
            damage.Init();

            foreach (var status in Statuses.List.Cast<Status>()) status.Init(this);
        }

        private void Save()
        {
            Data = PlayerData.NewData(this, Data);
        }
    }
}