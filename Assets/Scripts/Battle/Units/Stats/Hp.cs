using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Modifiers;

namespace Battle.Units.Stats
{
    [Serializable]
    public class Hp : Stat
    {
        private List<Modifier> hpMods = new();

        public Hp(int value, int borderUp, int borderDown = 0) : base(value, borderUp, borderDown) {}

        public Hp(int v, Stat stat) : base(v, stat) {}

        public Hp(Stat stat) : base(stat) {}

        public Hp(int v) : base(v) {}

        public int Heal(int val)
        {
            val = Math.Max(0, UseHpMods(val, ModClass.HpHealing));
            value += val;
            Norm();
            return val;
            // Possible logging
        }

        public int DoDamage(Damage dmg)
        {
            int doneDamage =
                UseHpMods(
                    ((DmgType[])Enum.GetValues(typeof(DmgType))).Sum(dmgType =>
                        UseHpMods(dmg.Parts[dmgType], ModClass.HpDamageTyped, dmgType)),
                    ModClass.HpDamageBase);
            value -= Math.Max(0, doneDamage);
            Norm();
            return doneDamage;
            // Possible logging
        }

        private int UseHpMods(int val, ModClass workPattern, DmgType type = DmgType.Physic)
        {
            if (hpMods == null) return val;

            var where = hpMods.Where(v => v.workPattern == workPattern && v.dmgType == type).ToList();
            float mulVal = 1 + where.Where(v => v.type == ModType.Mul).Sum(v => v.Use);
            int addVal = (int)where.Where(v => v.type == ModType.Add).Sum(v => v.Use);
            return (int)(val * mulVal) + addVal;
        }
        
        public void AddMod(Modifier mod)
        {
            hpMods ??= new List<Modifier>();
            hpMods.Add(mod);
        }
    }
}