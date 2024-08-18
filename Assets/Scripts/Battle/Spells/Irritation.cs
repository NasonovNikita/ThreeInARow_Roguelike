using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Irritation", menuName = "Spells/Irritation")]
    public class Irritation : Spell
    {
        [SerializeField] private int damageAdditions;
        [SerializeField] private int moves;

        public override string Description =>
            string.Format(descriptionKeyRef.Value, damageAdditions);

        protected override void Action()
        {
            UnitBelong.Statuses.Add(
                new Units.Statuses.Irritation(damageAdditions, moves));
        }
    }
}