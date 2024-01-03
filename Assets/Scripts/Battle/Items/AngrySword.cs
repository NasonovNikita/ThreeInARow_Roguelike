using Battle.BattleEventHandlers;
using Battle.Modifiers;
using Battle.Units;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "AngrySword", menuName = "Items/AngrySword")]
    public class AngrySword : Item
    {
        [SerializeField] private int hpLessThen;
        [SerializeField] private float bonus;
        public override void Use(Unit unitBelong)
        {
            new LessHpThen(hpLessThen, unitBelong,
                () => unitBelong.AddDamageMod(new Modifier(-1, ModType.Mul,
                    ModClass.DamageBase,  value: bonus)));
        }
    }
}