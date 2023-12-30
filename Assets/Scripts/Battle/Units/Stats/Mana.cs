using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Modifiers;

namespace Battle.Units.Stats
{
    [Serializable]
    public class Mana : Stat
    {
        private List<Modifier> manaMods = new ();

        public Mana(int value, int borderUp, int borderDown = 0) : base(value, borderUp, borderDown) {}

        public Mana(int v, Stat stat) : base(v, stat) {}

        public Mana(Stat stat) : base(stat) {}

        public Mana(int v) : base(v) {}

        public int Refill(int val)
        {
            val = Math.Max(0, UseManaMods<ManaRefillMod>(val));
            value += val;
            return val;
        }

        public int Waste(int val)
        {
            val = Math.Max(0, UseManaMods<ManaWasteMod>(val));
            value -= val;
            return val;
        }

        public void AddMod(Modifier mod)
        {
            manaMods.Add(mod);
        }

        private int UseManaMods<T>(int val)
        {
            if (manaMods == null) return val;
            
            var where = manaMods.Where(v => v.GetType() is T).ToList();
            float mulVal = 1 + where.Where(v => v.type == ModType.Mul).Sum(v => v.Use());
            int addVal = (int)where.Where(v => v.type == ModType.Add).Sum(v => v.Use());
            return (int)(val * mulVal) + addVal;
        }
    }

    public class ManaRefillMod : Modifier
    {
        public ManaRefillMod(int moves, ModType type, float value = 1, bool isPositive = true, Action onMove = null,
            bool delay = false) : base(moves, type, isPositive, value, onMove, delay)
        {
        }
    }

    public class ManaWasteMod : Modifier
    {
        public ManaWasteMod(int moves, ModType type, float value = 1, bool isPositive = true, Action onMove = null,
            bool delay = false) : base(moves, type, isPositive, value, onMove, delay)
        {
        }
    }
}