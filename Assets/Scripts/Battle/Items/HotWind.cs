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
            new EveryTurn(() => {
                for (int i = 0; i < manager.enemies.Count; i++)
                {
                    if (!Tools.RandomChance(chance) && !manager.enemies[i].IsBurning) continue;
                    if (i > 0) manager.enemies[i - 1].StartBurning(1);
                    if (i < manager.enemies.Count - 1) manager.enemies[i + 1].StartBurning(1);
                }
            });
        }
    }
}