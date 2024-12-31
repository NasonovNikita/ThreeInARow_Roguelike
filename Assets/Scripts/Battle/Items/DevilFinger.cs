using Battle.Units;
using Battle.Units.Statuses;
using Other;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(menuName = "Items/DevilFinger")]
    public class DevilFinger : Item
    {
        [SerializeField] private int value;

        public override string Description =>
            descriptionKeyRef.Value.IndexErrorProtectedFormat(value);

        public override void OnGet()
        {
            Player.Data.AddStatus(new Deal(value, true));
        }
    }
}