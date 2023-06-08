using Battle.Units;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "PoweredKick", menuName = "Spells/PoweredKick")]
    public class PoweredKick : Spell
    {
        [SerializeField] private Modifier damageMod;

        [SerializeField] private Modifier stunMod;
        public override void Cast()
        {
            if (CantCast()) return;
        
            unit.mana -= manaCost;
            damageMod.Use(unit);
            stunMod.Use(unit);
            if (unit is Player) manager.EndTurn();
        }
    }
}