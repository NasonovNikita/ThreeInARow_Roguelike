using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Singe", menuName = "Spells/Singe")]
    public class Singe : Spell
    {
        public override void Cast()
        {
            unitBelong.mana.Waste(useCost);
            LogUsage();
            manager.player.StartBurning(count);
        }
    }
}