using System;
using System.Collections.Generic;
using System.Linq;

namespace Battle.Modifiers.StatModifiers
{
    [Serializable]
    public abstract class StatModifier : Modifier
    {
        protected abstract int Modify(int val);
        
        public static int UseModList(IEnumerable<StatModifier> list, int val) =>
            list?.Aggregate(val, (current, mod) => mod.Modify(current)) ?? val;

        protected StatModifier(bool save) : base(save) {}
    }
}