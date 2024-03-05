using Battle.Units;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Healing", menuName = "Spells/Healing")]
    public class Healing : Spell
    {
        protected override void Action() => unitBelong.Heal((int)(unitBelong.hp.borderUp * value));

        public void MapCast()
        {
            if (Player.data.mana <= useCost) return;
            Player.data.mana.Waste(useCost);
            Player.data.hp.Heal((int) (Player.data.hp.value * value));
        }

        public override string Description => string.Format(descriptionKeyRef.Value, Other.Tools.Percents(value));
    }
}