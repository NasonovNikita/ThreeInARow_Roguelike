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
            unitBelong.AddHpMod(new MoveStatModifier(-1,
                ModType.Add,
                ModClass.HpDamageTyped,
                DmgType.Magic,
                value: -value,
                permanent: true));
        }

        public override string Title => titleKeyRef.Value;

        public override string Description => string.Format(descriptionKeyRef.Value, value);
        
    }
}