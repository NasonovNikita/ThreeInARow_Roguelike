using System.Collections.Generic;
using Battle.Items;
using Battle.Spells;
using Battle.Units.Modifiers;
using Battle.Units.Stats;
using Localization = Core.LocalizedStringsKeys;
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
        
        protected BattleManager manager;
        
        private int currentMovesCount;

        protected bool HasMoves => currentMovesCount > 0;
        public List<Modifier> Statuses => statuses;

        protected List<Modifier> statuses = new ();
        
        public virtual void Awake()
        {
            manager = FindFirstObjectByType<BattleManager>();

            Tools.InstantiateAll(spells);
            foreach (Spell spell in spells)
            {
                spell.Init(this);
            }
            
            RefillMoves();
        }

        protected void RefillMoves() => currentMovesCount = maxMoves;

        public virtual void WasteMove() => currentMovesCount -= 1;
        public void WasteAllMoves() => currentMovesCount = 0;

        public virtual void TakeDamage(int dmg)
        {
            int gotDamage = hp.TakeDamage(dmg);
            GotDamageLog.Log(this, gotDamage);

            if (hp <= 0) NoHp();
        }

        protected abstract void NoHp();
    }
}