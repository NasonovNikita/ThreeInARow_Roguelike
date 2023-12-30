using Battle.BattleEventHandlers;
using Battle.Modifiers;
using Battle.Units;
using Battle.Units.Stats;
using UnityEngine;

namespace Battle.Items
{
    public class AngrySword : Item
    {
        [SerializeField] private int hpLessThen;
        [SerializeField] private float bonus;
        public override void Use(Unit unitBelong)
        {
            new LessHpThen(hpLessThen, unitBelong,
                () => unitBelong.AddDamageMod(new DamageMod(-1, ModType.Mul, value: bonus)));
        }
    }
}