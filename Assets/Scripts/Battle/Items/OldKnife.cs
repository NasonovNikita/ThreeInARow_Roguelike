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

        public override void OnGet()
        {
            Player.data.damage.mods.Add(new DamageConstMod(value, true));
        }
    }
}