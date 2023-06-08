using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "MagicShield", menuName = "Spells/MagicShield")]
    public class MagicShield : Spell
    {
        [SerializeField] private Modifier shieldMod;
        public override void Cast()
        {
            if (CantCast()) return;
        
            unit.mana -= manaCost;
            shieldMod.Use(unit);
        }
    }
}