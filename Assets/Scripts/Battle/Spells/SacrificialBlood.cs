using Map.Vertexes;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "SacrificialBlood", menuName = "Spells/SacrificialBlood")]
    public class SacrificialBlood : Spell
    {
        public override void Cast()
        {
            if (((BattleVertex)Map.Map.CurrentVertex()).group.isBoss) return;
            if (CantCast()) return;

            manager.player.hp.DoDamage(new Damage(useCost));
            LogUsage();
            manager.Win();
        }
    }
}