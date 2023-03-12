using System;
using System.Collections.Generic;

[Serializable]
public class Modifier
{
    public int Moves;
    
    public readonly ModifierType Type;
    
    public readonly float Value;

    private readonly List<Modifier> _belongList;

    public Modifier(int moves, ModifierType type, List<Modifier> belong, float value = 0)
    {
        Moves = moves;
        Type = type;
        Value = value;
        _belongList = belong;
        ModifierManager.AllMods.Add(this);
    }
    public void Move()
    {
        Moves -= 1;
        if (Moves == 0)
        {
            _belongList.Remove(this);
            ModifierManager.AllMods.Remove(this);
        }
    }
}