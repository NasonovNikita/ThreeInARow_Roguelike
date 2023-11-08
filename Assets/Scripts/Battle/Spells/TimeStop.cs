using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeStop", menuName = "Spells/TimeStop")]
public class TimeStop : Spell
{
    public override void Cast()
    {
        if (CantCast()) return;
        
        manager.player.mana -= manaCost;
        
        foreach (Enemy enemy in manager.enemies)
        {
            enemy.stateModifiers.Add(new Modifier(moves, ModType.Stun, new List<Condition>()));
        }
    }
}