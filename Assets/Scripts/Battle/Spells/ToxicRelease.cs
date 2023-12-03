using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "ToxicRelease", menuName = "Spells/ToxicRelease")]
    public class ToxicRelease : Spell
    {
        public override void Cast()
        {
            if (CantCast()) return;

            manager.player.mana -= useCost;
            manager.target.StartPoisoning(count);
        }
    }
}