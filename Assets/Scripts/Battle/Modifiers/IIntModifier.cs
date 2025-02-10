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

        /// <summary>
        /// Applies all enabled <see cref="IIntModifier"/> modifiers in list to a given value.
        /// </summary>
        /// <returns>Modified value.</returns>
        public static int UseModList(IEnumerable<Modifier> list, int val)
        {
            // ReSharper disable once PossibleInvalidOperationException
            return (int)list?.Where(mod => mod is IIntModifier && !mod.EndedWork)
                .Select(mod => (IIntModifier)mod)
                .Aggregate(val, (current, mod) => mod.Modify(current));
        }
    }
}