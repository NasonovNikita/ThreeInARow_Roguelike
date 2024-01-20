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
                ((GotDamageLog)BattleLog.GetLastLog()).GetData.Item1
                    .AddHpMod(new Modifier(moves, ModType.Mul,
                        ModClass.HpDamageBase, isPositive: false, value: value));
            });
        }

        public override string Title => "Mace";

        public override string Description =>
            $"When you hit an enemy it then gets {Tools.Percents(value)}% more damage for {moves} moves (stackable)";
    }
}