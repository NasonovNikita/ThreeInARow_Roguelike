using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoweredKick", menuName = "Spells/PoweredKick")]
public class PoweredKick : Spell
{
    public override void Cast()
    {
        if (CantCast()) return;
        
        manager.player.mana -= manaCost;
        manager.player.statusModifiers.Add(new Modifier(moves, ModType.Stun, new List<Condition>()));
        ApplyToDamage(manager.player, new Modifier(moves + 1, ModType.Mul,new List<Condition>(), value), ModAffect.Get);
        manager.EndTurn();
    }
}