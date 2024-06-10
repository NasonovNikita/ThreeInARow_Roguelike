using Battle.Units;
using Battle.Units.StatModifiers;
using Other;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "OldKnife", menuName = "Items/OldKnife")]
    public class OldKnife : Item
    {
        [SerializeField] private int value;

        public override string Description => descriptionKeyRef.Value.IndexErrorProtectedFormat(value);

        public override void OnGet()
        {
            Player.data.damage.mods.Add(new DamageConstMod(value, true));
        }
    }
}