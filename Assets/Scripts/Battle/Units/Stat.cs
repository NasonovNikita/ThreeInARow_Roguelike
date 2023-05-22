using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField]
    public float borderDown;
    
    [SerializeField]
    public float borderUp;

    [SerializeField]
    private float value;

    public Dictionary<ModAffect, List<Modifier>> mods = new()
    {
        { ModAffect.Add , new List<Modifier>()},
        { ModAffect.Get, new List<Modifier>()},
        { ModAffect.Sub, new List<Modifier>()}
    };

    public void Init()
    {
        mods = new Dictionary<ModAffect, List<Modifier>>
        {
            { ModAffect.Add , new List<Modifier>()},
            { ModAffect.Get, new List<Modifier>()},
            { ModAffect.Sub, new List<Modifier>()}
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

    public void AddMod(Modifier mod, ModAffect affect)
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
        return UseMods(ModAffect.Get, value, mods);
    }

    private static float UseMods(ModAffect type, float value, Dictionary<ModAffect, List<Modifier>> mods)
    {
        float mulValue = 1 + mods[type].Sum(mod => mod.type == ModType.Mul ? mod.Use() : 0);
        int addValue = (int) mods[type].Sum(mod => mod.type == ModType.Add ? mod.Use() : 0);
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
        n = UseMods(ModAffect.Add, n, stat.mods);
        return new Stat(stat.value + n, stat.borderUp, stat.borderDown);
    }

    public static Stat operator - (Stat stat, float n)
    {
        n = UseMods(ModAffect.Sub, n, stat.mods);
        return new Stat(stat.value - n, stat.borderUp, stat.borderDown);
    }

    public static explicit operator int(Stat stat)
    {
        return (int) stat.value;
    }

    //This two are for Warnings, hate them.
    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}