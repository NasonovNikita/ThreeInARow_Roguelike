using Battle.Modifiers.Statuses;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Inferno", menuName = "Spells/Inferno")]
    public class Inferno : Spell
    {
        [SerializeField] private int dmg;
        protected override void Action()
        {
            foreach (var enemy in unitBelong.Enemies)
            {
                enemy.TakeDamage(dmg);
                enemy.AddStatus(new Burning());
            }
        }

        public override string Description => descriptionKeyRef.Value;
    }
}