using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;

namespace Battle.Units
{
    [Serializable]
    public class Stat
    {
        protected bool Equals(Stat other)
        {
            return borderDown.Equals(other.borderDown) && borderUp.Equals(other.borderUp) && value.Equals(other.value) && Equals(mods, other.mods);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Stat)obj);
        }

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            return HashCode.Combine(borderDown, borderUp, value, mods);
        }

        [SerializeField]
        public float borderDown;
    
        [SerializeField]
        public float borderUp;

        [SerializeField]
        private float value;

        public Dictionary<FuncAffect, List<Modifier>> mods = new()
        {
            { FuncAffect.Add , new List<Modifier>()},
            { FuncAffect.Get, new List<Modifier>()},
            { FuncAffect.Sub, new List<Modifier>()}
        };

        public void Init()
        {
            mods = new Dictionary<FuncAffect, List<Modifier>>
            {
                { FuncAffect.Add , new List<Modifier>()},
                { FuncAffect.Get, new List<Modifier>()},
                { FuncAffect.Sub, new List<Modifier>()}
            };
        }

        public Stat(float value, float borderUp, float borderDown = 0)
        {
            this.value = value;
            this.borderUp = borderUp;
            this.borderDown = borderDown;
            Norm();
        }

        public Stat(int v, Stat stat)
        {
            value = v;
            borderUp = stat.borderUp;
            borderDown = stat.borderDown;
            Norm();
        }

        public Stat(Stat stat)
        {
            value = stat.borderUp;
            borderUp = stat.borderUp;
            borderDown = stat.borderDown;
        }

        public Stat(int v)
        {
            value = v;
            borderUp = value;
            borderDown = 0;
        }

        public void AddMod(Modifier mod, FuncAffect affect)
        {
            mods[affect].Add(mod);
        }

        private void Norm()
        {
            if (value < borderDown)
            {
                value = borderDown;
            }

            if (value > borderUp)
            {
                value = borderUp;
            }
        }

        public float GetValue()
        {
            return UseMods(FuncAffect.Get, value, mods);
        }

        private static float UseMods(FuncAffect type, float value, IReadOnlyDictionary<FuncAffect, List<Modifier>> mods)
        {
            float mulValue = 1 + mods[type].Sum(mod => mod.Type == ModType.Mul ? mod.Value : 0);
            int addValue = (int) mods[type].Sum(mod => mod.Type == ModType.Add ? mod.Value : 0);
            return value * mulValue + addValue;
        }

        public static bool operator == (Stat stat, float n)
        {
            return stat?.value == n;
        }

        public static bool operator != (Stat stat, float n)
        {
            return stat?.value == n;
        }

        public static bool operator >= (Stat stat, int n)
        {
            return stat != null && stat.value - stat.borderDown >= n;
        }

        public static bool operator <= (Stat stat, int n)
        {
            return stat != null && stat.value - stat.borderDown <= n;
        }

        public static bool operator > (Stat stat, int n)
        {
            return stat != null && stat.value - stat.borderDown > n;
        }

        public static bool operator < (Stat stat, int n)
        {
            return stat != null && stat.value - stat.borderDown < n;
        }

        public static Stat operator + (Stat stat, float n)
        {
            n = UseMods(FuncAffect.Add, n, stat.mods);
            return new Stat(stat.value + n, stat.borderUp, stat.borderDown);
        }

        public static Stat operator - (Stat stat, float n)
        {
            n = UseMods(FuncAffect.Sub, n, stat.mods);
            return new Stat(stat.value - n, stat.borderUp, stat.borderDown);
        }

        public static explicit operator int(Stat stat)
        {
            return (int) stat.value;
        }
    }

    public enum UnitStat
    {
        Hp,
        Mana,
        Damage
    }
}