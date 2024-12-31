using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Hurricane", menuName = "Spells/Hurricane")]
    public class Hurricane : Spell
    {
        public override string Description => descriptionKeyRef.Value;

        protected override void Action()
        {
            BattleFlowManager.ShuffleEnemies();
        }
    }
}