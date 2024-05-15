using Battle.Modifiers.StatModifiers;
using Battle.Units;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "MagicStaff", menuName = "Items/MagicStaff")]
    public class MagicStaff : Item
    {
        [SerializeField] private int value;

        public override string Title => titleKeyRef.Value;

        public override string Description => string.Format(descriptionKeyRef.Value, value);

        public override void OnGet()
        {
            Player.data.mana.wastingMods.Add(new ManaWastingConstMod(-value, true));
        }
    }
}