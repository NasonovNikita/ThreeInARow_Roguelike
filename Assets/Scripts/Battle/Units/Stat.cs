using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Stat
{
    [SerializeField]
    public int borderDown;
    
    [SerializeField]
    public int borderUp;

    [SerializeField]
    private int value;

    public List<Modifier> onAddMods = new ();

    public List<Modifier> onSubMods = new ();

    public List<Modifier> onGetMods = new ();

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

    public int GetValue()
    {
        return UseMods(onGetMods, value);
    }

    private static int UseMods(List<Modifier> mods, int value)
    {
        float mulValue = 1 + mods.Sum(modifier => modifier.Type == ModifierType.Mul ? modifier.Value : 0);
        int addValue = (int) mods.Sum(modifier => modifier.Type == ModifierType.Add ? modifier.Value : 0);
        return (int) (value * mulValue) + addValue;
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
        n = UseMods(stat.onAddMods, n);
        return new Stat(stat.value + n, stat.borderUp, stat.borderDown);
    }

    public static Stat operator - (Stat stat, int n)
    {
        n = UseMods(stat.onSubMods, n);
        return new Stat(stat.value - n, stat.borderUp, stat.borderDown);
    }
}