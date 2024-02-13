using System.Collections;
using System.Linq;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Inferno", menuName = "Spells/Inferno")]
    public class Inferno : Spell
    {
        public override IEnumerator Cast()
        {
            if (CantCastOrCast()) yield break;

            Damage dmg = new Damage(fDmg: (int) value);
            foreach (var enemy in manager.enemies.Where(v => v != null))
            {
                enemy.TakeDamage(dmg);
                enemy.StartBurning(count);

                //PToEDamageLog.Log(enemy, manager.player, dmg);
            }

            yield return Wait();
        }

        public override string Description => descriptionKeyRef.Value;
    }
}