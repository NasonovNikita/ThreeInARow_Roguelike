using Battle.Modifiers.StatModifiers;
using Battle.Units;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "OldKnife", menuName = "Items/OldKnife")]
    public class OldKnife : Item
    {
        [SerializeField] private int value;
        
        public override string Title => titleKeyRef.Value;

        public override string Description => string.Format(descriptionKeyRef.Value, value);

        public override void Get()
        {
            Player.data.damage.AddMod(new DamageMod(value, true));
            base.Get();
        }
    }
}