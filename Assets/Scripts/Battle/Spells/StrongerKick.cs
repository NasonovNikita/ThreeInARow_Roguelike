using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "StrongerKick", menuName = "Spells/StrongerKick")]
    public class StrongerKick : Spell
    {
        [SerializeField] private Modifier damageMod;
        public override void Cast()
        {
            if (CantCast()) return;
        
            unit.mana -= manaCost;
            damageMod.Use(unit);
        }
    }
}