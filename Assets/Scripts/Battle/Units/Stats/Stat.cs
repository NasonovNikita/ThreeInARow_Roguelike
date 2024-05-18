using System;
using UnityEngine;
using Random = UnityEngine.Random;
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

        protected int FixedValueChange(int change) => 
            change >= 0 ? Math.Min(BorderUp - value, change) : -Math.Min(value, -change);

        public void UnAttach()
        {
            OnValueChanged = null;
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

        public override int GetHashCode() => HashCode.Combine(ToString());

        #endregion
    }
}