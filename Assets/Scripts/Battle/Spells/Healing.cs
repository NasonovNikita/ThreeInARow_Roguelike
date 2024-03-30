using Battle.Units;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Healing", menuName = "Spells/Healing")]
    public class Healing : Spell
    {
        [SerializeField] private int healAmount;
        
        protected override void Action() =>
            unitBelong.hp.Heal(healAmount);

        public void MapCast()
        {
            if (Player.data.mana <= useCost) return;
            Player.data.mana.Waste(useCost);
            Player.data.hp.Heal(healAmount);
        }

        public override string Description =>
            string.Format(descriptionKeyRef.Value, healAmount);
    }
}