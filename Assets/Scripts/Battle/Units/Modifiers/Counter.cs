using System;

namespace Battle.Units.Modifiers
{
    [Serializable]
    public abstract class Counter : Modifier
    {
        protected int count;

        protected Counter(int count,
            bool permanent = false) : base(permanent) =>
            this.count = count;

        public string SubInfo => count.ToString();

        public bool IsZero => count == 0;

        public virtual void TurnOff()
        {
            if (!permanent) count = 0;
        }

        public void Concat(IModifier other) =>
            count += ((Counter)other).count;

        protected int Decrease(int val)
        {
            int res = Math.Max(val - count, 0);
            count = Math.Max(count - val, 0);
            
            return res;
        }

        protected int Increase(int val)
        {
            int res = val + count;
            TurnOff();
            
            return res;
        }
    }
}