using System.Collections.Generic;
using Battle.Modifiers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "MagicShield", menuName = "Spells/MagicShield")]
    public class MagicShield : Spell
    {
        public override void Cast()
        {
            if (CantCast()) return;
        
            manager.player.mana -= useCost;
            manager.player.hp.AddMod(new Modifier(count, ModType.Mul, value: value), ModAffect.ValueSub);
        }
    }
}