using Battle.Modifiers;
using Battle.Units;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "OldKnife", menuName = "Items/OldKnife")]
    public class OldKnife : Item
    {
        [SerializeField] private int value;
        public override void Use(Unit unitBelong) {}

        
        public override string Title => titleKeyRef.Value;

        public override string Description => string.Format(descriptionKeyRef.Value, value);

        public override void OnGet()
        {
            Player.data.AddDamageMod(new Modifier(-1, ModType.Add,
                ModClass.DamageTypedStat, DmgType.Physic, value: value));
        }
    }
}