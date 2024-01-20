using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Modifiers;

namespace Battle.Units.Stats
{
    [Serializable]
    public class Mana : Stat
    {
        private List<Modifier> mods = new ();

        public Mana(int value, int borderUp, int borderDown = 0) : base(value, borderUp, borderDown) {}

        public Mana(int v, Stat stat) : base(v, stat) {}

        public Mana(Stat stat) : base(stat) {}

        public Mana(int v) : base(v) {}

        public int Refill(int val)
        {
            val = Math.Max(0, UseManaMods(val, ModClass.ManaRefill));
            value += val;
            Norm();
            return val;
        }

        public int Waste(int val)
        {
            val = Math.Max(0, UseManaMods(val, ModClass.ManaWaste));
            value -= val;
            return val;
        }

        public void AddMod(Modifier mod)
        {
            mods ??= new List<Modifier>();
            mods.Add(mod);
        }

        private int UseManaMods(int val, ModClass workPattern)
        {
            if (mods == null) return val;
            
            var where = mods.Where(v => v.workPattern == workPattern).ToList();
            float mulVal = 1 + where.Where(v => v.type == ModType.Mul).Sum(v => v.Use);
            int addVal = (int)where.Where(v => v.type == ModType.Add).Sum(v => v.Use);
            return (int)(val * mulVal) + addVal;
        }
    }
}