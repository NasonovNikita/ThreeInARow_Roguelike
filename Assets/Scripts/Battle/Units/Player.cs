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
    [Serializable]
    public class Player : Unit
    {
        public static PlayerData data;
        [SerializeField] private int maxMoves;
        private int currentMovesCount;

        public static Player Instance { get; private set; }

        public override List<Unit> Enemies => new(BattleFlowManager.Instance.EnemiesWithoutNulls);

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

        public void RefillMoves()
        {
            currentMovesCount = maxMoves;
        }

        public void WasteAllMoves()
        {
            currentMovesCount = 0;
        }

        public void AddMove()
        {
            currentMovesCount++;
        }

        public void AddMoves(int count)
        {
            currentMovesCount += count;
        }

        public void WasteMove()
        {
            currentMovesCount -= 1;
        }

        public void StartTurn()
        {
            BattleFlowManager.Instance.endedProcesses.Add(() => currentMovesCount == 0);

            if (statuses.ModList.Exists(mod => mod is Stun { EndedWork: false }))
                currentMovesCount = 0;
        }

        private void Load()
        {
            hp = data.hp;
            mana = data.mana;
            damage = data.damage;

            spells = new List<Spell>(data.spells);
            statuses = new ModifierList(data.statuses);
        }

        public void LateLoad()
        {
            hp.Init();
            mana.Init();
            damage.Init();

            foreach (Status status in statuses.ModList.Cast<Status>()) status.Init(this);
        }

        private void Save()
        {
            data = PlayerData.NewData(this, data);
        }
    }
}