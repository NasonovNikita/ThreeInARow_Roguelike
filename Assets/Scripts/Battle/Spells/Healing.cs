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
                if (CantCast()) return;
                unitBelong.mana.Waste(useCost);
                LogUsage();
                unitBelong.hp.Heal((int) (unitBelong.hp.value * value));
            }
        }

        public override string Title => "Healing";

        public override string Description => $"Heal {Other.Tools.Percents(value)}% of maximum hp";
    }
}