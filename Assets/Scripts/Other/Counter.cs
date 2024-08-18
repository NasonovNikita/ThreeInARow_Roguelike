using System;
using UnityEngine;

namespace Other
{
    /// <summary>
    ///     Counter is used to spend its value to <see cref="Increase"/>/
    ///     <see cref="Decrease"/> other value.
    /// </summary>
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

        /// Mark for garbage collection/disabling dependent objects.
        public bool EndedWork => count == 0;

        public event Action OnChanged;

        public void ConcatWith(Counter other)
        {
            if (other.count == 0) return; // Not invoking OnChanged
            count += other.count;
            if (count < 0) count = 0;

            OnChanged?.Invoke();
        }

        /// <summary>
        ///     Spend value of Counter to decrease given value up to zero.
        ///     <example>
        ///         If Counter has value of 5 and given value of 4 it will return 0 and Counter's value will become 5-4=1.<br/>
        ///         If Counter has value of 5 and given value of 7 it will return 7-5=2 and Counter's value will become 0.
        ///     </example>
        /// </summary>
        /// <param name="val">The "given value".</param>
        /// <returns>Decreased value.</returns>
        public int Decrease(int val)
        {
            if (EndedWork) return val; // Not invoking OnChanged

            var res = Math.Max(val - count, 0);
            count = Math.Max(count - val, 0);

            OnChanged?.Invoke();

            return res;
        }

        /// <summary>
        ///     Increases given value by Counter's value and sets Counter's value to 0.
        ///     Basically you get val + Counter.count
        /// </summary>
        /// <param name="val">The "given value"</param>
        /// <returns>Increased value.</returns>
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