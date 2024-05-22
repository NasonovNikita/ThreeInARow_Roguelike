using Battle.Modifiers.Statuses;
using Battle.Units;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Inferno", menuName = "Spells/Inferno")]
    public class Inferno : Spell
    {
        [SerializeField] private int dmg;
        protected override void Action()
        {
            foreach (Unit enemy in unitBelong.Enemies)
            {
                enemy.TakeDamage(dmg);
                enemy.Statuses.Add(new Burning());
            }
        }

        public override string Description => descriptionKeyRef.Value;
    }
}