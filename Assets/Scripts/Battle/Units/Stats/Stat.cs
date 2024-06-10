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

        public void ChangeBorderUp(int dBorder, int dValue = 0)
        {
            borderUp += dBorder;

            dValue = FixedValueChange(dValue);
            value += dValue;

            OnValueChanged?.Invoke(dValue);
        }

        public void ChangeValue(int change)
        {
            change = FixedValueChange(change);
            value += change;

            OnValueChanged?.Invoke(change);
        }

        protected int FixedValueChange(int change)
        {
            return change >= 0 ? Math.Min(BorderUp - value, change) : -Math.Min(value, -change);
        }

        public void UnAttach()
        {
            OnValueChanged = null;
        }


        #region Initializers

        protected Stat(int value, int borderUp)
        {
            this.value = value;
            this.borderUp = borderUp;
        }

        protected Stat(int v, Stat stat)
        {
            value = v;
            borderUp = stat.borderUp;
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

        #region operators

        public static bool operator ==(Stat stat, int n)
        {
            return stat?.value == n;
        }

        public static bool operator !=(Stat stat, int n)
        {
            return stat?.value == n;
        }

        public static bool operator >=(Stat stat, int n)
        {
            return stat.value >= n;
        }

        public static bool operator <=(Stat stat, int n)
        {
            return stat.value <= n;
        }

        public static bool operator >(Stat stat, int n)
        {
            return stat.value > n;
        }

        public static bool operator <(Stat stat, int n)
        {
            return stat.value < n;
        }

        public static explicit operator int(Stat stat)
        {
            return stat.value;
        }

        protected bool Equals(Stat stat)
        {
            return ReferenceEquals(this, stat);
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) &&
                   (ReferenceEquals(this, obj) || (obj.GetType() == GetType() && Equals((Stat)obj)));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ToString());
        }

        #endregion
    }
}