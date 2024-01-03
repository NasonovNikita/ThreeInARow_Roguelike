using Battle.Units;
using Battle.Units.Stats;
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
                Player.data.unitHp.Heal((int) (Player.data.unitHp.value * value));
            }
            else
            {
                if (CantCast()) return;
                attachedUnit.mana.Waste(useCost);
                LogUsage();
                attachedUnit.Hp.Heal((int) (attachedUnit.Hp.value * value));
            }
        }
    }
}