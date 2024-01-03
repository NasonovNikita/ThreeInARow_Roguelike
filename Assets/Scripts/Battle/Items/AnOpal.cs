using Battle.BattleEventHandlers;
using Battle.Modifiers;
using Battle.Units;
using Battle.Units.Stats;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "AnOpal", menuName = "Items/AnOpal")]
    public class AnOpal : Item
    {
        [SerializeField] private int moves;
        [SerializeField] private float value;
        public override void Use(Unit unitBelong)
        {
            new EnemyGettingHitThen(() =>
            {
                ((GotDamageLog)BattleLog.GetLastLog()).GetData.Item1
                    .AddHpMod(new Modifier(moves, ModType.Mul,
                        ModClass.DamageBase, isPositive: false, value: value));
            });
        }
    }
}