using System.Linq;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Inferno", menuName = "Spells/Inferno")]
    public class Inferno : Spell
    {
        public override void Cast()
        {
            if (CantCastOrCast()) return;

            Damage dmg = new Damage(fDmg: (int) value);
            foreach (var enemy in manager.enemies.Where(v => v != null))
            {
                enemy.DoDamage(dmg);
                enemy.StartBurning(count);

                PToEDamageLog.Log(enemy, manager.player, dmg);
            }
        }

        public override string Title => titleKeyRef.Value;

        public override string Description => descriptionKeyRef.Value;
    }
}