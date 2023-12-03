using System.Linq;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Hurricane", menuName = "Spells/Hurricane")]
    public class Hurricane : Spell
    {
        public override void Cast()
        {
            manager.enemies = manager.enemies.OrderBy(_ => Random.Range(0, 10000000)).ToList();
            manager.OnEnemiesShuffle();
        }
    }
}