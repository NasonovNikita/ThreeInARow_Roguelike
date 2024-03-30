using System.Collections.Generic;
using System.Linq;

namespace Battle.Modifiers
{
    public interface IConcatAble
    {
        public bool ConcatAbleWith(IConcatAble other);

        public void Concat(IConcatAble other);

        public static void AddToList<T>(List<T> list, T other)
        {
            if (other is not IConcatAble first) list.Add(other);
            else
            {
                var second = (IConcatAble)list.FirstOrDefault(obj =>
                    obj is IConcatAble concatAble && concatAble.ConcatAbleWith(first));
                if (second is not null) second.Concat(first);
                else list.Add(other);
            }
        }
    }
}