using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "SacrificialBlood", menuName = "Spells/SacrificialBlood")]
    public class SacrificialBlood : Spell
    {
        protected override void Action()
        {
            manager.Win();
        }

        protected override void Waste()
        {
            unitBelong.hp.StraightChange(-useCost);
        }

        protected override bool CantCast => NotAllowedTurn || unitBelong.hp <= useCost;

        public override string Description => descriptionKeyRef.Value;
    }
}