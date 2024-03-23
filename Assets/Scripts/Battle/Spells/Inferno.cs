using System.Linq;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Inferno", menuName = "Spells/Inferno")]
    public class Inferno : Spell
    {
        protected override void Action()
        {
            Damage dmg = new Damage(fDmg: (int) value);
            foreach (var enemy in manager.Enemies.Where(v => v != null))
            {
                enemy.TakeDamage(dmg);
                enemy.StartBurning(count);
            }
        }

        public override string Description => descriptionKeyRef.Value;
    }
}