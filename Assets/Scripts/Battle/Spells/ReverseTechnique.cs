using System.Collections;
using System.Linq;
using Battle.Modifiers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "ReverseTechnique", menuName = "Spells/ReverseTechnique")]
    public class ReverseTechnique : Spell
    {
        public override IEnumerator Cast()
        {
            if (CantCastOrCast()) yield break;

            foreach (var mod in unitBelong.allMods.Where(mod => !mod.isPositive && !mod.always))
            {
                mod.TurnOff();
            }

            yield return Wait();
        }

        public override string Description => descriptionKeyRef.Value;
    }
}