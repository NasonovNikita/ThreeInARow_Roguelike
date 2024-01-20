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

        public override string Title => throw new System.NotImplementedException();

        public override string Description => throw new System.NotImplementedException();
    }
}