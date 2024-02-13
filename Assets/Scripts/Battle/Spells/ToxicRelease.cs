using System.Collections;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "ToxicRelease", menuName = "Spells/ToxicRelease")]
    public class ToxicRelease : Spell
    {
        public override IEnumerator Cast()
        {
            if (CantCastOrCast()) yield break;

            unitBelong.Target.StartPoisoning(count);

            yield return Wait();
        }

        public override string Description => string.Format(descriptionKeyRef.Value, count);
    }
}