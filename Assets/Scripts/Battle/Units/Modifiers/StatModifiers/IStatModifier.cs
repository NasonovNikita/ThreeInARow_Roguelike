using System.Collections.Generic;
using System.Linq;
using UI.Battle;

namespace Battle.Units.Modifiers.StatModifiers
{
    public interface IStatModifier
    {
        protected int Modify(int val);
        
        public static int UseModList(IEnumerable<IStatModifier> list, int val)
            => list.Aggregate(val, (current, mod) => mod.Modify(current));
    }
}