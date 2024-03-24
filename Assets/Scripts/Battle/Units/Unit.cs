using System.Collections.Generic;
using Battle.Spells;
using Battle.Units.Modifiers.Statuses;
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
        
        protected BattleManager manager;
        
        private int currentMovesCount;

        protected bool HasMoves => currentMovesCount > 0;
        public List<Status> Statuses => statuses;

        protected List<Status> statuses = new ();
        
        public virtual void Awake()
        {
            manager = FindFirstObjectByType<BattleManager>();

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