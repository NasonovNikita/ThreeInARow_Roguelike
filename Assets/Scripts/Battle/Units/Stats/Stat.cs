using System;
using SerializeField = UnityEngine.SerializeField;

namespace Battle.Units.Stats
{
    [Serializable]
    public abstract class Stat
    {
        [SerializeField] private int borderUp;
        [SerializeField] private int value;
        public int Value => value;
        public int BorderUp => borderUp;
        
        public event Action<int> OnValueChanged;


        #region Initializers

        protected Stat(int value, int borderUp)
        {
            this.value = value;
            this.borderUp = borderUp;
            FixInvariant();
        }
        
        protected Stat(int v, Stat stat)
        {
            value = v;
            borderUp = stat.borderUp;
            FixInvariant();
        }

        protected Stat(Stat stat)
        {
            value = stat.borderUp;
            borderUp = stat.borderUp;
        }

        protected Stat(int v)
        {
            value = v;
            borderUp = value;
        }
        
        #endregion

        public void StraightChange(int val)
        {
            value += val;
            FixInvariant();
        }

        public void ChangeBorderUp(int dBorder, int dValue = 0)
        {
            borderUp += dBorder;
            value += dValue;
            FixInvariant();
        }

        protected void ChangeValue(int change)
        {
            value += change;
            OnValueChanged?.Invoke(change);
            FixInvariant();
        }

        protected void FixInvariant()
        {
            if (value > borderUp) value = borderUp;
            if (value < 0) value = 0;
        }

        #region operators
        
        public static bool operator == (Stat stat, int n) => stat?.value == n;

        public static bool operator != (Stat stat, int n) => stat?.value == n;

        public static bool operator >= (Stat stat, int n) => stat.value >= n;

        public static bool operator <= (Stat stat, int n) => stat.value <= n;

        public static bool operator > (Stat stat, int n) => stat.value > n;

        public static bool operator < (Stat stat, int n) => stat.value < n;

        public static explicit operator int(Stat stat) => stat.value;

        protected bool Equals(Stat stat) => ReferenceEquals(this, stat);

        public override bool Equals(object obj) =>
            !ReferenceEquals(null, obj) &&
            (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((Stat)obj));

        public override int GetHashCode() => throw new NotImplementedException();

        #endregion
    }
}