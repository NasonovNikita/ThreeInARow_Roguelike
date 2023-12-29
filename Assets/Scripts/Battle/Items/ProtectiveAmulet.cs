using Battle.Modifiers;
using Battle.Units;
using Battle.Units.Stats;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "ProtectiveAmulet", menuName = "Items/ProtectiveAmulet")]
    public class ProtectiveAmulet : Item
    {
        [SerializeField] private int value;
        public override void Use(Unit unitBelong)
        {
            unitBelong.AddHpMod(new TypedDamageMod(-1, ModType.Add, DmgType.Magic, value: value));
        }
    }
}