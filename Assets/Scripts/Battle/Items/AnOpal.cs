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
                ((PToEDamageLog)BattleLog.GetLastLog()).GetData().Item1
                    .AddHpMod(new DamageMod(moves, ModType.Mul, false, -value));
            });
        }
    }
}