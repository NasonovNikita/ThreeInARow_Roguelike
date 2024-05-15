using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Hurricane", menuName = "Spells/Hurricane")]
    public class Hurricane : Spell
    {
        protected override void Action() => battleFlowManager.ShuffleEnemies();

        public override string Description => descriptionKeyRef.Value;
    }
}