using System;
using UnityEngine;

namespace Battle.Units.Modifiers
{
    [Serializable]
    public class Counter : IConcatAble
    {
        [SerializeField] protected int count;

        public Counter(int count) => this.count = count;

        public bool EndedWork => count == 0;
        public string SubInfo => count.ToString();

        public int Decrease(int val)
        {
            int res = Math.Max(val - count, 0);
            count = Math.Max(count - val, 0);
            
            return res;
        }

        public int Increase(int val)
        {
            int res = val + count;
            count = 0;
            
            return res;
        }

        public bool ConcatAbleWith(IConcatAble other) => other is Counter;

        public void Concat(IConcatAble other) => count += ((Counter)other).count;
    }
}