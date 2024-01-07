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

            manager.player.Hp.DoDamage(new Damage(useCost));
            LogUsage();
            manager.Win();
        }
    }
}