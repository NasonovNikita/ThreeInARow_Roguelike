using Battle.Modifiers;
using Battle.Units.Stats;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "PoweredKick", menuName = "Spells/PoweredKick")]
    public class PoweredKick : Spell
    {
        public override void Cast()
        {
            if (CantCast()) return;
        
            manager.player.mana.Waste(useCost);
            manager.player.AddMod(new Modifier(count, ModType.Stun));
            manager.player.AddDamageMod(new DamageMod(count + 1, ModType.Mul,value: value));
            manager.EndTurn();
        }
    }
}