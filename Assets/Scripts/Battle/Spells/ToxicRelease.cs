using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "ToxicRelease", menuName = "Spells/ToxicRelease")]
    public class ToxicRelease : Spell
    {
        public override void Cast()
        {
            if (CantCast()) return;

            manager.player.mana.Waste(useCost);
            LogUsage();
            manager.player.Target.StartPoisoning(count);
        }

        public override string Title => titleKeyRef.Value;

        public override string Description => descriptionKeyRef.Value;
    }
}