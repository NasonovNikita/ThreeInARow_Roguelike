using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicShield", menuName = "Spells/MagicShield")]
public class MagicShield : Spell
{
    public override void Cast()
    {
        if (CantCast()) return;
        
        manager.player.mana -= manaCost;
        manager.player.hp.AddMod(new Modifier(moves, ModType.Mul, new List<Condition>(), value), ModAffect.Sub);
    }
}