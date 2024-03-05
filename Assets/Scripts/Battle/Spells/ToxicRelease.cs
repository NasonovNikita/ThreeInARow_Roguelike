using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "ToxicRelease", menuName = "Spells/ToxicRelease")]
    public class ToxicRelease : Spell
    {
        protected override void Action()
        {
            unitBelong.Target.StartPoisoning(count);
        }

        public override string Description => string.Format(descriptionKeyRef.Value, count);
    }
}