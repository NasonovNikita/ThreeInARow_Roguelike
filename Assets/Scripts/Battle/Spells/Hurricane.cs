using System.Collections;
using System.Linq;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Hurricane", menuName = "Spells/Hurricane")]
    public class Hurricane : Spell
    {
        public override IEnumerator Cast()
        {
            if (CantCastOrCast()) yield break;

            manager.enemies = manager.enemies.OrderBy(_ => Random.Range(0, 10000000)).ToList();
            manager.PlaceEnemies();

            yield return Wait();
        }

        public override string Description => descriptionKeyRef.Value;
    }
}