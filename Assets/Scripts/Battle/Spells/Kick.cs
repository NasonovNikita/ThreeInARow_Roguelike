using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Kick", menuName = "Spells/Kick")]
    public class Kick : Spell
    {
        [SerializeField] private int damage;
        protected override void Action() => unitBelong.target.hp.TakeDamage(damage);

        public override string Description => string.Format(descriptionKeyRef.Value, damage);
    }
}