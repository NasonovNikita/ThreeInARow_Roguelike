using System;
using System.Collections.Generic;
using Battle.Modifiers;
using Battle.Modifiers.Statuses;
using Battle.Spells;
using Battle.Units.Stats;
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
        
        protected BattleManager manager;
        
        private int currentMovesCount;

        protected bool HasMoves => currentMovesCount > 0;
        public ModifierList<Status> Statuses => statuses;

        protected ModifierList<Status> statuses = new ();

        public event Action OnSpellCasted;
        public event Action OnMadeHit;
        public event Action OnDied;
        
        public bool Dead { get; private set; }
        
        public void UseSpell() => OnSpellCasted?.Invoke();
        public void MakeHit() => OnMadeHit?.Invoke();
        
        public virtual void Awake()
        {
            manager = FindFirstObjectByType<BattleManager>();

            Tools.InstantiateAll(spells);
            foreach (var spell in spells)
            {
                spell.Init(this);
            }

            foreach (var status in statuses.ModList)
            {
                status.Init(this);
            }
            
            RefillMoves();
        }

        public void AddStatus(Status status)
        {
            statuses.Add(status);
        }

        protected void RefillMoves() => currentMovesCount = maxMoves;

        public virtual void WasteMove() => currentMovesCount -= 1;
        public void WasteAllMoves() => currentMovesCount = 0;

        public virtual void TakeDamage(int dmg)
        {
            hp.TakeDamage(dmg);

            if (hp <= 0) NoHp();
        }

        protected virtual void NoHp()
        {
            Die();
        }

        private void Die()
        {
            Dead = true;
            OnDied?.Invoke();
            Destroy(gameObject);
        }
    }
}