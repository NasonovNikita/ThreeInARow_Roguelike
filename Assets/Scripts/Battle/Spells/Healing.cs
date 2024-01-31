using Battle.Units;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Healing", menuName = "Spells/Healing")]
    public class Healing : Spell
    {
        public override void Cast()
        {
            if (SceneManager.GetActiveScene().name == "Map")
            {
                if (Player.data.mana <= useCost) return;
                Player.data.mana.Waste(useCost);
                Player.data.hp.Heal((int) (Player.data.hp.value * value));
            }
            else
            {
                if (CantCastOrCast()) return;
                unitBelong.Heal((int) (unitBelong.hp.borderUp * value));
            }
        }

        public override string Title => titleKeyRef.Value;

        public override string Description => string.Format(descriptionKeyRef.Value, Other.Tools.Percents(value));
    }
}