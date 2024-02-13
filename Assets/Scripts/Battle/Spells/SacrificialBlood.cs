using System.Collections;
using Map.Vertexes;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "SacrificialBlood", menuName = "Spells/SacrificialBlood")]
    public class SacrificialBlood : Spell
    {
        public override IEnumerator Cast()
        {
            if (((BattleVertex)Map.Map.CurrentVertex()).isBoss || CantCastOrCast()) yield break;

            manager.player.TakeDamage(new Damage(useCost));
            LogUsage();
            manager.Win();

            yield return Wait();
        }

        public override string Description => descriptionKeyRef.Value;
    }
}