using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "ArrowOfLight", menuName = "Spells/ArrowOfLight")]
    public class ArrowOfLight : Spell
    {
        protected override void Action()
        {
            unitBelong.target.TakeDamage((int)value);
        }

        public override string Description => string.Format(descriptionKeyRef.Value, (int) value);
    }
}