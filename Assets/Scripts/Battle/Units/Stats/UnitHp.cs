using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Modifiers;
using Unity.Mathematics;

namespace Battle.Units.Stats
{
    [Serializable]
    public class UnitHp : Stat
    {
        private List<Modifier> hpMods = new();

        public UnitHp(int value, int borderUp, int borderDown = 0) : base(value, borderUp, borderDown) {}

        public UnitHp(int v, Stat stat) : base(v, stat) {}

        public UnitHp(Stat stat) : base(stat) {}

        public UnitHp(int v) : base(v) {}

        public int Heal(int val)
        {
            val = Math.Max(0, UseHpMods<HealingMod>(val));
            value += val;
            Norm();
            return val;
            // Possible logging
        }

        public int DoDamage(Damage dmg)
        {
            int doneDamage = UseHpMods<DamageMod>(((DmgType[])Enum.GetValues(typeof(DmgType))).Sum(dmgType =>
                UseHpMods<TypedDamageMod>(dmg.Get()[dmgType], dmgType)));
            value -= Math.Max(0, doneDamage);
            Norm();
            return doneDamage;
            // Possible logging
        }

        private int UseHpMods<T>(int val, DmgType type = DmgType.Physic)
        {
            if (hpMods == null) return val;
            
            var where = hpMods.Where(v =>
                    v.GetType() is T && (!(typeof(T) == typeof(TypedDamageMod)) || ((TypedDamageMod)v).dmgType == type))
                .ToList();
            float mulVal = 1 + where.Where(v => v.type == ModType.Mul).Sum(v => v.Use());
            int addVal = (int)where.Where(v => v.type == ModType.Add).Sum(v => v.Use());
            return (int)(val * mulVal) + addVal;
        }
        
        public void AddMod(Modifier mod)
        {
            hpMods ??= new List<Modifier>();
            hpMods.Add(mod);
        }
    }

    public class HealingMod : Modifier
    {
        public HealingMod(int moves, ModType type, bool isPositive = true, float value = 1, Action onMove = null,
            bool delay = false) : base(moves, type, isPositive, value, onMove, delay)
        {
            
        }
    }
}