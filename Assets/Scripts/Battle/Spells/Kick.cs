using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Kick", menuName = "Spells/Kick")]
    public class Kick : Spell
    {
        [SerializeField] private int damage;

        public override string Description => string.Format(descriptionKeyRef.Value, damage);

        protected override void Action()
        {
            unitBelong.target.TakeDamage(damage);
        }
    }
}