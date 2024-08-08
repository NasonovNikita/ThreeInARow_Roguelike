using Battle.Units;
using Battle.Units.StatModifiers;
using Other;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "PlainShield", menuName = "Items/PlainShield")]
    public class PlainShield : Item
    {
        [SerializeField] private int value;

        public override string Description =>
            descriptionKeyRef.Value.IndexErrorProtectedFormat(value);

        public override void OnGet()
        {
            Player.Data.hp.onTakingDamageMods.Add(new HpDamageConstMod(-value, true));
        }
    }
}