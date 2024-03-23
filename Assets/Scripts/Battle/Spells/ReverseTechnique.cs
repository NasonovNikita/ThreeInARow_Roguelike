using System.Linq;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "ReverseTechnique", menuName = "Spells/ReverseTechnique")]
    public class ReverseTechnique : Spell
    {
        protected override void Action()
        {
            foreach (var mod in unitBelong.statuses.Where(mod => !mod.isPositive && !mod.always))
            {
                mod.TurnOff();
            }
        }

        public override string Description => descriptionKeyRef.Value;
    }
}