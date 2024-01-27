using System.Linq;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Hurricane", menuName = "Spells/Hurricane")]
    public class Hurricane : Spell
    {
        public override void Cast()
        {
            if (CantCast()) return;

            unitBelong.mana.Waste(useCost);
            LogUsage();
            manager.enemies = manager.enemies.OrderBy(_ => Random.Range(0, 10000000)).ToList();
            manager.OnEnemiesShuffle();
        }

        public override string Title => titleKeyRef.Value;

        public override string Description => descriptionKeyRef.Value;
    }
}