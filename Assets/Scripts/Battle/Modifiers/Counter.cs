using System;
using Other;
using UnityEngine;

namespace Battle.Modifiers
{
    [Serializable]
    public class Counter : IChangeAble
    {
        [SerializeField] private int count;

        public Counter(int count)
        {
            this.count = count;
        }

        public int Count => count;

        public string SubInfo => count.ToString();

        public event Action OnChanged;

        public bool EndedWork => count == 0;

        public void Concat(Counter other)
        {
            if (other.count == 0) return; // Not invoking OnChanged
            count += other.count;
            if (count < 0) count = 0;

            OnChanged?.Invoke();
        }

        public int Decrease(int val)
        {
            if (EndedWork) return val; // Not invoking OnChanged

            var res = Math.Max(val - count, 0);
            count = Math.Max(count - val, 0);

            OnChanged?.Invoke();

            return res;
        }

        public int Increase(int val)
        {
            if (EndedWork) return val;

            var res = val + count;
            count = 0;

            OnChanged?.Invoke();

            return res;
        }

        public static Counter operator +(Counter first, Counter second)
        {
            return new Counter(first.count + second.count);
        }
    }
}