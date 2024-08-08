using Battle.Units;
using Battle.Units.StatModifiers;
using Other;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "MagicStaff", menuName = "Items/MagicStaff")]
    public class MagicStaff : Item
    {
        [SerializeField] private int value;

        public override string Description =>
            descriptionKeyRef.Value.IndexErrorProtectedFormat(value);

        public override void OnGet()
        {
            Player.Data.mana.wastingMods.Add(new ManaWastingConstMod(-value, true));
        }
    }
}