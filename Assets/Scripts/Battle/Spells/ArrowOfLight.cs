using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "ArrowOfLight", menuName = "Spells/ArrowOfLight")]
    public class ArrowOfLight : Spell
    {
        protected override void Action()
        {
            manager.target.TakeDamage(new Damage(lDmg: (int)value));
        }

        public override string Description => string.Format(descriptionKeyRef.Value, (int) value);
    }
}