using Battle.Modifiers;
using Battle.Units;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "ProtectiveAmulet", menuName = "Items/ProtectiveAmulet")]
    public class ProtectiveAmulet : Item
    {
        [SerializeField] private int value;
        public override void Use(Unit unitBelong)
        {
            unitBelong.AddHpMod(new Modifier(-1, ModType.Add, ModClass.DamageTyped, DmgType.Magic, value: -value));
        }

        public override string Title => "Protective Amulet";

        public override string Description => "-5 magic dmg (useless haha)";
    }
}