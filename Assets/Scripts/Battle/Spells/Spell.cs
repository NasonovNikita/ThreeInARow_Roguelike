using System;
using Battle.Modifiers;
using Battle.Units;
using Battle.Units.Stats;
using Shop;
using UnityEngine;

namespace Battle.Spells
{
    [Serializable]
    public abstract class Spell : ScriptableObject, IGood
    {
        [SerializeField] public int useCost;

        [SerializeField] private string title;
        [SerializeField] private string description;

        [SerializeField] protected float value;

        [SerializeField] protected int count;

        protected Unit attachedUnit;

        protected BattleManager manager;
        public string Title => title;
        public string Description => description;

        public void Init(Unit unit)
        {
            attachedUnit = unit;
            manager = FindFirstObjectByType<BattleManager>();
        }

        public abstract void Cast();

        protected void LogUsage()
        {
            SpellUsageLog.Log(attachedUnit, useCost);
        }

        protected bool CantCast()
        {
            return manager.State != BattleState.Turn || manager.player.mana < useCost;
        }
        
        public virtual void OnBuy() {}
    }
}