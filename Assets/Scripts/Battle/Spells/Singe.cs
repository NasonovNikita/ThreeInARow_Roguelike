using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Singe", menuName = "Spells/Singe")]
    public class Singe : Spell
    {
        public override void Cast()
        {
            attachedUnit.mana.Waste(useCost);
            manager.player.StartBurning(1);
        }
    }
}