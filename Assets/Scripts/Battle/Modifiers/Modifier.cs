using System;
using System.Collections.Generic;

[Serializable]
public class Modifier
{
    public int moves;
    
    public readonly ModifierType Type;
    
    public float value;

    private readonly List<Modifier> _belongList;

    private Func<bool> _cond;

    public Modifier(int moves, ModifierType type, List<Modifier> belong, Func<bool> cond, float value = 0)
    {
        this.moves = moves;
        Type = type;
        this.value = value;
        _belongList = belong;
        _cond = cond;
        ModifierManager.AllMods.Add(this);
    }

    public float Use()
    {
        return _cond() ? value : 0;
    }
    public void Move()
    {
        moves -= 1;
        if (moves != 0) return;
        _belongList.Remove(this);
        ModifierManager.AllMods.Remove(this);
    }
}