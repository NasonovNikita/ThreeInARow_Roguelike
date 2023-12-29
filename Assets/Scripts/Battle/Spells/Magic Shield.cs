using Battle.Modifiers;
using Battle.Units.Stats;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "MagicShield", menuName = "Spells/MagicShield")]
    public class MagicShield : Spell
    {
        public override void Cast()
        {
            if (CantCast()) return;
        
            manager.player.mana.Waste(useCost);
            manager.player.AddHpMod(new DamageMod(count, ModType.Mul, value: value));
        }
    }
}