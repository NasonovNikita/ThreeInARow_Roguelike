using System;
using System.Collections.Generic;
using Battle.Modifiers;
using Battle.Modifiers.Statuses;
using Battle.Spells;
using Battle.Units.Stats;
using Core.Singleton;
using Tools = Other.Tools;
using UnityEngine;

namespace Battle.Units
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] private int maxMoves;
        
        public Hp hp;
        public Mana mana;
        public Damage damage;
        
        public List<Spell> spells;
        
        public int manaPerGem;

        public Unit target;
        public abstract List<Unit> Enemies { get; }
        
        protected BattleFlowManager battleFlowManager;
        
        private int currentMovesCount;

        protected bool HasMoves => currentMovesCount > 0;
        public ModifierList<Status> Statuses => statuses;

        protected ModifierList<Status> statuses = new();

        public event Action OnSpellCasted;
        public event Action OnMadeHit;
        public event Action OnDied;
        
        public bool Dead { get; private set; }
        
        public void UseSpell() => OnSpellCasted?.Invoke();
        public void InvokeOnMadeHit() => OnMadeHit?.Invoke();
        
        public virtual void Awake()
        {
            battleFlowManager = FindFirstObjectByType<BattleFlowManager>();

            Tools.InstantiateAll(spells);

            hp.OnValueChanged += _ => CheckHp();
            
            foreach (Spell spell in spells)
            {
                spell.Init(this);
            }

            foreach (Status status in statuses.ModList)
            {
                status.Init(this);
            }
            
            RefillMoves();
        }

        protected void RefillMoves() => currentMovesCount = maxMoves;

        public virtual void WasteMove() => currentMovesCount -= 1;
        public void WasteAllMoves() => currentMovesCount = 0;

        private void CheckHp()
        {
            if (hp <= 0) Die();
        }

        public void Die()
        {
            if (Dead) return;
            
            Dead = true;
            OffScreenPoint.Instance.Hide(gameObject);
            OnDied?.Invoke();
        }
    }
}