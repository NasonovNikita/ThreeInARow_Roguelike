using Battle.BattleEventHandlers;
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
                if (BattleLog.AllLogs
                    .Exists(log => (log as SpellUsageLog)?.GetData.Item1 is Player)) return;
                Player.data.AddDamageMod(new MoveStatModifier(-1, ModType.Add, 
                    ModClass.DamageTypedStat, value: value, permanent: true));
            });
        }

        
        public override string Title => titleKeyRef.Value;

        public override string Description => string.Format(descriptionKeyRef.Value, value);

    }
}