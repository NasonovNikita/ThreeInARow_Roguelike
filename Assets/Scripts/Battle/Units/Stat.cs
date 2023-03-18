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

    public List<Modifier> onAddMods = new ();

    public List<Modifier> onSubMods = new ();

    public List<Modifier> onGetMods = new ();

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
        return UseMods(onGetMods, value);
    }

    private static float UseMods(List<Modifier> mods, float value)
    {
        float mulValue = 1 + mods.Sum(modifier => modifier.Type == ModifierType.Mul ? modifier.Use() : 0);
        int addValue = (int) mods.Sum(modifier => modifier.Type == ModifierType.Add ? modifier.Use() : 0);
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
        n = UseMods(stat.onAddMods, n);
        return new Stat(stat.value + n, stat.borderUp, stat.borderDown);
    }

    public static Stat operator - (Stat stat, float n)
    {
        n = UseMods(stat.onSubMods, n);
        return new Stat(stat.value - n, stat.borderUp, stat.borderDown);
    }
}