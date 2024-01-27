using Battle.BattleEventHandlers;
using Battle.Units;
using Other;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "HotWind", menuName = "Items/HotWind")]
    public class HotWind : Item
    {
        [SerializeField] private int chance;
        public override void Use(Unit unitBelong)
        {
            BattleManager manager = FindFirstObjectByType<BattleManager>();
            var enemies = manager.enemies;
            new EveryTurn(() => {
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (enemies[i] == null || !enemies[i].IsBurning || !Tools.Random.RandomChance(chance)) continue;
                    if (i > 0 && enemies[i - 1] != null) enemies[i - 1].StartBurning(1);
                    if (i < enemies.Count - 1 && enemies[i + 1] != null) enemies[i + 1].StartBurning(1);
                }
            });
        }

        
        public override string Title => titleKeyRef.Value;

        public override string Description => string.Format(descriptionKeyRef.Value, chance);
    }
}