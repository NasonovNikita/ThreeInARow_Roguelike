using System.Collections.Generic;
using Battle.Modifiers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "StrongerKick", menuName = "Spells/StrongerKick")]
    public class StrongerKick : Spell
    {
        public override void Cast()
        {
            if (CantCast()) return;
        
            manager.player.mana -= useCost;
            manager.player.phDmg.AddMod(new Modifier(count, ModType.Mul, value: value), ModAffect.ValueGet);
        }
    }
}