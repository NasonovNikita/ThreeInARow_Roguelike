using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "ArrowOfLight", menuName = "Spells/ArrowOfLight")]
    public class ArrowOfLight : Spell
    {
        [SerializeField] private int dmg;
        protected override void Action() =>
            unitBelong.target.TakeDamage(dmg);

        public override string Description =>
            string.Format(descriptionKeyRef.Value, dmg);
    }
}