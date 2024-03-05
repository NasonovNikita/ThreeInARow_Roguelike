using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Kick", menuName = "Spells/Kick")]
    public class Kick : Spell
    {
        protected override void Action() => manager.target.TakeDamage(new Damage(mDmg: (int) value));

        public override string Description => string.Format(descriptionKeyRef.Value, value);
    }
}