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
            if (CantCastOrCast()) return;

            manager.player.DoDamage(new Damage(useCost));
            LogUsage();
            manager.Win();
        }

        public override string Title => titleKeyRef.Value;

        public override string Description => descriptionKeyRef.Value;
    }
}