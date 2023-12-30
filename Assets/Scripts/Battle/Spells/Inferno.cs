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
                Damage dmg = new Damage(fDmg: (int) value);
                enemy.DoDamage(dmg);

                PToEDamageLog.Log(enemy, manager.player, dmg);
            }
        }
    }
}