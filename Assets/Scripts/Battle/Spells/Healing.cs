using Battle.Units;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Healing", menuName = "Spells/Healing")]
    public class Healing : Spell
    {
        [SerializeField] private int healAmount;

        public override string Description =>
            string.Format(descriptionKeyRef.Value, healAmount);

        protected override void Action()
        {
            unitBelong.hp.Heal(healAmount);
        }

        public void MapCast()
        {
            if (Player.Data.mana <= useCost) return;
            Player.Data.mana.Waste(useCost);
            Player.Data.hp.Heal(healAmount);
        }
    }
}