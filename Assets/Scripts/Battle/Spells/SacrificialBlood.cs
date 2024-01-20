using Map.Vertexes;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "SacrificialBlood", menuName = "Spells/SacrificialBlood")]
    public class SacrificialBlood : Spell
    {
        public override void Cast()
        {
            if (((BattleVertex)Map.Map.CurrentVertex()).isBoss) return;
            if (CantCast()) return;

            manager.player.hp.DoDamage(new Damage(useCost));
            LogUsage();
            manager.Win();
        }

        public override string Title => "Sacrificial Blood";

        public override string Description => "NO ONE ESCAPES";
    }
}