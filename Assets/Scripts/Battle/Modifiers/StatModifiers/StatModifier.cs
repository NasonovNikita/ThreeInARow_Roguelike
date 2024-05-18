using System;
using System.Collections.Generic;
using System.Linq;

namespace Battle.Modifiers.StatModifiers
{
    [Serializable]
    public abstract class StatModifier : Modifier
    {
        protected abstract int Modify(int val);

        public static int UseModList(IEnumerable<Modifier> list, int val) =>
            // ReSharper disable once PossibleInvalidOperationException
            (int)list?.Select(mod => (StatModifier)mod)
                .Aggregate(val, (current, mod) => mod.Modify(current));

        protected StatModifier(bool save) : base(save) {}
    }
}