using System.Collections.Generic;
using System.Linq;

namespace Battle.Units.Modifiers
{
    public interface ISaveAble
    {
        public bool Save { get; }

        public static List<T> SaveList<T>(IEnumerable<T> list) =>
            list.Where(obj => obj is ISaveAble { Save: true }).ToList();
    }
}