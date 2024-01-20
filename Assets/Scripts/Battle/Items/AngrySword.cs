using Battle.BattleEventHandlers;
using Battle.Modifiers;
using Battle.Units;
using Other;
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

        public override string Title => "Angry Sword";

        public override string Description => $"If you have less then {hpLessThen} hp you deal {Tools.Percents(bonus)}% more dmg";
    }
}