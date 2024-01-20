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

        public override string Title => "Toxic Release";

        public override string Description => $"Enemy gets poisoned for {count} turns";
    }
}