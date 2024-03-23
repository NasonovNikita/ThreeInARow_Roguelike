using System.Linq;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Hurricane", menuName = "Spells/Hurricane")]
    public class Hurricane : Spell
    {
        protected override void Action()
        {
            manager.Enemies = manager.Enemies.OrderBy(_ => Random.Range(0, 10000000)).ToList();
            manager.PlaceEnemies();
        }

        public override string Description => descriptionKeyRef.Value;
    }
}