using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Modifier
{
    private static List<Modifier> _mods = new();

    public int moves;

    public ModType type;
    
    public float value;

    [SerializeField]
    private List<Condition> conditions;

    public Modifier(int moves, ModType type, List<Condition> conditions, float value = 0)
    {
        this.moves = moves;
        this.type = type;
        this.conditions = conditions;
        this.value = value;
        _mods.Add(this);
    }

    public float Use()
    {
        bool res = conditions.Aggregate(true, (current, cond) => current && cond.Use());
        return res ? value : 0;
    }
    public static void Move()
    {
        foreach (Modifier mod in _mods.ToList())
        {
            mod.moves -= 1;
            if (mod.moves == 0)
            {
                _mods.Remove(mod);
            }
        }
    }
}