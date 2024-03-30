using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Hurricane", menuName = "Spells/Hurricane")]
    public class Hurricane : Spell
    {
        protected override void Action() => manager.ShuffleEnemies();

        public override string Description => descriptionKeyRef.Value;
    }
}