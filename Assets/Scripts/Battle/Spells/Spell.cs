using System;
using Battle.Modifiers;
using Battle.Units;
using Battle.Units.Stats;
using Other;
using Shop;
using UnityEngine;

namespace Battle.Spells
{
    [Serializable]
    public abstract class Spell : LootItem
    {
        [SerializeField] public int useCost;

        [SerializeField] protected float value;

        [SerializeField] protected int count;

        protected Unit unitBelong;

        protected BattleManager manager;

        public void Init(Unit unit)
        {
            unitBelong = unit;
            manager = FindFirstObjectByType<BattleManager>();
        }

        public override void Get()
        {
            Player.data.spells.Add(this);
            base.Get();
        }

        public abstract void Cast();

        protected void LogUsage()
        {
            SpellUsageLog.Log(unitBelong, useCost);
        }

        protected bool CantCastOrCast()
        {
            bool cantCast =  unitBelong switch
            {
                Player => manager.State != BattleState.Turn || unitBelong.mana < useCost,
                Enemy => unitBelong.mana < useCost,
                _ => throw new ArgumentOutOfRangeException()
            };
            if (cantCast) return true;

            unitBelong.WasteMana(useCost);
            LogUsage();
            return false;
        }
    }
}