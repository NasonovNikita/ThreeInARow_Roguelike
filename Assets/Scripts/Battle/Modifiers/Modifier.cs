using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Modifier
{
    private static List<Modifier> _mods = new();

    public int moves;

    public ModType type;
    
    public float value;

    private Func<bool> cond;

    public Modifier(int moves, ModType type, Func<bool> cond, float value = 0)
    {
        this.moves = moves;
        this.type = type;
        this.cond = cond;
        this.value = value;
        _mods.Add(this);
    }

    public float Use()
    {
        return cond() ? value : 0;
    }
    public static void Move()
    {
        foreach (Modifier mod in _mods)
        {
            mod.moves -= 1;
            if (mod.moves <= 0)
            {
                _mods.Remove(mod);
            }
        }
    }
}