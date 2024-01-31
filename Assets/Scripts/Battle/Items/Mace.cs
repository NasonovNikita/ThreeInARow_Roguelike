using Battle.BattleEventHandlers;
using Battle.Modifiers;
using Battle.Units;
using Other;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "Mace", menuName = "Items/AnOpal")]
    public class Mace : Item
    {
        [SerializeField] private int moves;
        [SerializeField] private float value;
        public override void Use(Unit unitBelong)
        {
            new EnemyGettingHitThen(() =>
            {
                ((GotDamageLog)BattleLog.LastLog).GetData.Item1
                    .AddHpMod(new Modifier(moves, ModType.Mul,
                        ModClass.HpDamageBase, isPositive: false, value: value));
            });
        }

        
        public override string Title => titleKeyRef.Value;

        public override string Description => string.Format(descriptionKeyRef.Value, Tools.Percents(value), moves);
    }
}