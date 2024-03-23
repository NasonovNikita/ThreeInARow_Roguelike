using System;
using Battle.Units.Modifiers;
using Battle.Units.Modifiers.StatModifiers;
using UI.Battle;
using SerializeField = UnityEngine.SerializeField;

namespace Battle.Units.Stats
{
    [Serializable]
    public abstract class Stat
    {
        [SerializeField] protected HUDSpawner hud;
        [SerializeField] protected ModIconGrid modsGrid;
        
        [SerializeField] protected int borderDown;
        [SerializeField] protected int borderUp;
        [SerializeField] protected int value;

        public int Value => value;
        public int BorderUp => borderUp;
        public int BorderDown => borderDown;

        #region Initializers

        protected Stat(int value, int borderUp, int borderDown = 0)
        {
            this.value = value;
            this.borderUp = borderUp;
            this.borderDown = borderDown;
            FixInvariant();
        }
        
        protected Stat(int v, Stat stat)
        {
            value = v;
            borderUp = stat.borderUp;
            borderDown = stat.borderDown;
            FixInvariant();
        }

        protected Stat(Stat stat)
        {
            value = stat.borderUp;
            borderUp = stat.borderUp;
            borderDown = stat.borderDown;
        }

        protected Stat(int v)
        {
            value = v;
            borderUp = value;
            borderDown = 0;
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

        protected void AddModToGrid(IStatModifier mod) => modsGrid.Add(mod);

        protected void FixInvariant()
        {
            if (value < borderDown) value = borderDown;

            if (value > borderUp) value = borderUp;
        }

        #region operators
        
        public static bool operator == ( Stat stat, int n) => stat?.value == n;

        public static bool operator != (Stat stat, int n) => stat?.value == n;

        public static bool operator >= (Stat stat, int n) => stat.value - stat.borderDown >= n;

        public static bool operator <= (Stat stat, int n) => stat.value - stat.borderDown <= n;

        public static bool operator > (Stat stat, int n) => stat.value - stat.borderDown > n;

        public static bool operator < (Stat stat, int n) => stat.value - stat.borderDown < n;

        public static explicit operator int(Stat stat) => stat.value;

        protected bool Equals(Stat stat) => ReferenceEquals(this, stat);

        public override bool Equals(object obj) =>
            !ReferenceEquals(null, obj) &&
            (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((Stat)obj));

        public override int GetHashCode() => throw new NotImplementedException();

        #endregion
    }
}