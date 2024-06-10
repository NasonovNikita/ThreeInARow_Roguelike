using Battle.Units;
using Battle.Units.Statuses;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Inferno", menuName = "Spells/Inferno")]
    public class Inferno : Spell
    {
        [SerializeField] private int dmg;

        public override string Description => descriptionKeyRef.Value;

        protected override void Action()
        {
            foreach (Unit enemy in unitBelong.Enemies)
            {
                enemy.TakeDamage(dmg);
                enemy.Statuses.Add(new Burning());
            }
        }
    }
}