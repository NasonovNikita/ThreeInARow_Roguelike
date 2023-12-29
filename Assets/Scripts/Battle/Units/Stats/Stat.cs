using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Modifiers;
using UnityEngine;

namespace Battle.Units.Stats
{
    [Serializable]
    public class Stat
    {
        [SerializeField]
        public int borderDown;
    
        [SerializeField]
        public int borderUp;

        [SerializeField]
        public int value;

        public Stat(int value, int borderUp, int borderDown = 0)
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

        protected void Norm()
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

        protected static int UseMods(float value, List<Modifier> mods)
        {
            float mulValue = 1 + mods.Sum(mod => mod.type == ModType.Mul ? mod.Use() : 0);
            int addValue = (int) mods.Sum(mod => mod.type == ModType.Add ? mod.Use() : 0);

            return (int) (value * mulValue + addValue);
        }

        public static bool operator == (Stat stat, int n)
        {
            return stat?.value == n;
        }

        public static bool operator != (Stat stat, int n)
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

        
        public static Stat operator + (Stat stat, int n)
        {
            return new Stat(stat.value + n, stat);
        }

        public static Stat operator - (Stat stat, int n)
        {
            return new Stat(stat.value - n, stat);
        }

        public static explicit operator int(Stat stat)
        {
            return stat.value;
        }
    }
}