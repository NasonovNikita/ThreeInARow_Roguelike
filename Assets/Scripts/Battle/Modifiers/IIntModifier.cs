using System.Collections.Generic;
using System.Linq;

namespace Battle.Modifiers
{
    /// <summary>
    ///     Modifiers of this kind change given value by simple Int value.
    /// </summary>
    /// <seealso cref="UseModList"/>
    public interface IIntModifier
    {
        protected int Modify(int val);

        /// Applies all Modifiers in given list to a given value.
        public static int UseModList(IEnumerable<Modifier> list, int val)
        {
            // ReSharper disable once PossibleInvalidOperationException
            return (int)list?.Where(mod => mod is IIntModifier && !mod.EndedWork)
                .Select(mod => (IIntModifier)mod)
                .Aggregate(val, (current, mod) => mod.Modify(current));
        }
    }
}