using System;
using System.Collections.Generic;
using Audio;
using Battle.Modifiers;
using Battle.Modifiers.Statuses;
using Battle.Spells;

namespace Battle.Units
{
    [Serializable]
    public class Player : Unit
    {
        public static Player Instance { get; private set; }
        
        public static PlayerData data;

        public override List<Unit> Enemies => new(battleFlowManager.EnemiesWithoutNulls);

        public override void Awake()
        {
            Instance = this;
            
            hp.OnValueChanged += _ => AudioManager.Instance.Play(AudioEnum.PlayerHit);
            
            Load();
            base.Awake();
        }

        public void Start()
        {
            LateLoad();
        }

        public void OnDestroy() => Save();

        public override void WasteMove()
        {
            base.WasteMove();
            if (HasMoves) return;
            
            battleFlowManager.StartEnemiesTurn();
            RefillMoves();
        }

        public void StartTurn()
        {
            if (HasMoves) return;
            battleFlowManager.StartEnemiesTurn();
        }

        private void Load()
        {
            hp = data.hp;
            mana = data.mana;
            damage = data.damage;
            
            manaPerGem = data.manaPerGem;
            spells = new List<Spell>(data.spells);
            statuses = new ModifierList(data.statuses);
        }

        private void LateLoad()
        {
            hp.Init();
            mana.Init();
            damage.Init();
        }

        private void Save() => data = PlayerData.NewData(this, data);
    }
}