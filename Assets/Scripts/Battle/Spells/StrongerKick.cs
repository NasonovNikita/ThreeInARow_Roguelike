using System.Collections.Generic;
using Battle;
using Battle.Modifiers;
using UnityEngine;

[CreateAssetMenu(fileName = "StrongerKick", menuName = "Spells/StrongerKick")]
public class StrongerKick : Spell
{
    public override void Cast()
    {
        if (CantCast()) return;
        
        manager.player.mana -= manaCost;
        ApplyToDamage(manager.player, new Modifier(moves, ModType.Mul, new List<Condition>(), value), ModAffect.Get);
    }
}