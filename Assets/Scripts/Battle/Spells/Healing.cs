using System.Collections;
using Battle.Units;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Healing", menuName = "Spells/Healing")]
    public class Healing : Spell
    {
        public override IEnumerator Cast()
        {
            if (SceneManager.GetActiveScene().name == "Map")
            {
                if (Player.data.mana <= useCost) yield break;
                Player.data.mana.Waste(useCost);
                Player.data.hp.Heal((int) (Player.data.hp.value * value));
            }
            else
            {
                if (CantCastOrCast()) yield break;
                unitBelong.Heal((int) (unitBelong.hp.borderUp * value));

                yield return Wait();
            }
        }

        public override string Description => string.Format(descriptionKeyRef.Value, Other.Tools.Percents(value));
    }
}