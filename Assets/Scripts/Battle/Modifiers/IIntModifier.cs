using System.Collections.Generic;
using System.Linq;

namespace Battle.Modifiers
{
    public interface IIntModifier
    {
        protected int Modify(int val);

        public static int UseModList(IEnumerable<Modifier> list, int val)
        {
            // ReSharper disable once PossibleInvalidOperationException
            return (int)list?.Where(mod => mod is IIntModifier).Select(mod => (IIntModifier)mod)
                .Aggregate(val, (current, mod) => mod.Modify(current));
        }
    }
}