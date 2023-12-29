using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Inferno", menuName = "Spells/Inferno")]
    public class Inferno : Spell
    {
        public override void Cast()
        {
            if (CantCast()) return;

            manager.player.mana.Waste(useCost);
            foreach (var enemy in manager.enemies)
            {
                enemy.DoDamage(new Damage(fDmg: (int) value));
            }
        }
    }
}