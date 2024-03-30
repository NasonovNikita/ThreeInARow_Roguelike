using System;
using System.Collections.Generic;
using Battle.Modifiers;
using Battle.Modifiers.Statuses;
using Battle.Spells;
using Battle.Units.Stats;
using UI.Battle;
using Tools = Other.Tools;
using UnityEngine;

namespace Battle.Units
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] private int maxMoves;
        [SerializeField] private HUDSpawner hud;
        [SerializeField] private ModIconGrid modsGrid;
        
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
        public List<Status> Statuses => statuses;

        protected List<Status> statuses = new ();

        public Action OnSpellCasted;
        public Action OnMadeHit;
        public void UseSpell() => OnSpellCasted?.Invoke();
        public void MakeHit() => OnMadeHit?.Invoke();
        
        public virtual void Awake()
        {
            manager = FindFirstObjectByType<BattleManager>();
            
            hp.Init(hud, modsGrid);
            mana.Init(hud, modsGrid);
            damage.Init(hud, modsGrid);

            Tools.InstantiateAll(spells);
            foreach (var spell in spells)
            {
                spell.Init(this);
            }

            foreach (var status in statuses)
            {
                status.Init(this);
            }
            
            RefillMoves();
        }

        public void AddStatus(Status status)
        {
            IConcatAble.AddToList(statuses, status);
            if (statuses.Contains(status)) modsGrid.Add(status);
        }

        protected void RefillMoves() => currentMovesCount = maxMoves;

        public virtual void WasteMove() => currentMovesCount -= 1;
        public void WasteAllMoves() => currentMovesCount = 0;

        public virtual void TakeDamage(int dmg)
        {
            int gotDamage = hp.TakeDamage(dmg);

            if (hp <= 0) NoHp();
        }

        protected abstract void NoHp();
    }
}