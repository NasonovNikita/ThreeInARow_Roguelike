using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Irritation", menuName = "Spells/Irritation")]
    public class Irritation : Spell
    {
        [SerializeField] private int damageAdditions;
        [SerializeField] private int moves;
        
        protected override void Action() =>
            unitBelong.Statuses.Add(new Modifiers.Statuses.Irritation(damageAdditions, moves));

        public override string Description =>
            string.Format(descriptionKeyRef.Value, damageAdditions);
    }
}