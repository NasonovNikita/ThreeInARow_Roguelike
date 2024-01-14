using Battle.BattleEventHandlers;
using Battle.Modifiers;
using Battle.Units;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "DevilFinger", menuName = "Items/DevilFinger")]
    public class DevilFinger : Item
    {
        [SerializeField] private int value;
        public override void Use(Unit unitBelong)
        {
            new BattleEndThen(() =>
            {
                if (BattleLog.GetAllLogs()
                    .Exists(log => (log as SpellUsageLog)?.GetData.Item1 is Player)) return;
                Player.data.AddDamageMod(new Modifier(-1, ModType.Add, 
                    ModClass.DamageTypedStat, value: value, always: true));
            });
        }
    }
}