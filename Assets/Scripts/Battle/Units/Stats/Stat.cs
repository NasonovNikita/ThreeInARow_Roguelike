using System;
using SerializeField = UnityEngine.SerializeField;

namespace Battle.Units.Stats
{
    /// <summary>
    ///     Its value is not less than zero and is less than fixed value (<see cref="BorderUp"/>).
    ///     Supports <c>int</c> operations besides multiplication.
    /// </summary>
    [Serializable]
    public abstract class Stat
    {
        [SerializeField] private int borderUp;
        [SerializeField] private int value;
        public int Value => value;
        public int BorderUp => borderUp;

        public event Action<int> OnValueChanged;

        /// <summary>
        ///     Protected border change. Keeps border not less than zero.
        /// </summary>
        /// <seealso cref="ChangeValue"/>
        public void ChangeBorderUp(int dBorder)
        {
            borderUp += dBorder;
            borderUp = borderUp >= 0 ? borderUp : 0;
        }

        /// <summary>
        ///     Protected value change that keeps the value not less than zero
        ///     and less than fixed value.<br/>Also invokes <see cref="OnValueChanged"/>.
        /// </summary>
        /// <seealso cref="ChangeBorderUp"/>
        public void ChangeValue(int change)
        {
            change = FixedValueChange(change); // Get the actually applying change
            value += change;

            OnValueChanged?.Invoke(change);
        }

        /// <summary>
        ///     Deletes all existing <see cref="OnValueChanged"/> event listeners.
        /// </summary>
        public void UnAttach()
        {
            OnValueChanged = null;
        }

        protected int FixedValueChange(int change) =>
            change >= 0
                ? Math.Min(BorderUp - value, change)
                : -Math.Min(value, -change);

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

        public static bool operator ==(Stat stat, int n) => stat?.value == n;

        public static bool operator !=(Stat stat, int n) => stat?.value == n;

        public static bool operator >=(Stat stat, int n) => stat.value >= n;

        public static bool operator <=(Stat stat, int n) => stat.value <= n;

        public static bool operator >(Stat stat, int n) => stat.value > n;

        public static bool operator <(Stat stat, int n) => stat.value < n;

        public static explicit operator int(Stat stat) => stat.value;

        protected bool Equals(Stat stat) => ReferenceEquals(this, stat);

        public override bool Equals(object obj) =>
            !ReferenceEquals(null, obj) && (ReferenceEquals(this, obj) ||
                                            (obj.GetType() == GetType() &&
                                             Equals((Stat)obj)));

        public override int GetHashCode() => HashCode.Combine(ToString());

        #endregion
    }
}