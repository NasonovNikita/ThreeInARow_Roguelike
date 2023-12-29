using Battle.Units.Stats;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "SacrificialBlood", menuName = "Spells/SacrificialBlood")]
    public class SacrificialBlood : Spell
    {
        public override void Cast()
        {
            if (CantCast()) return;

            manager.player.unitHp = (UnitHp) (manager.player.unitHp - (int)value);
            manager.Win();
        }
    }
}