using System.Collections.Generic;
using System.Linq;

public static class ModifierManager
{
    public static readonly List<Modifier> AllMods = new ();

    public static void Move()
    {
        foreach (Modifier mod in AllMods.ToList())
        { 
            mod.Move();
        }
    }
}