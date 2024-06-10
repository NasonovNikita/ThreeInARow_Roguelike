using Battle.Units;
using Battle.Units.Statuses;
using Other;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(menuName = "Items/Mace")]
    public class Mace : Item
    {
        [SerializeField] private int addition;

        public override string Description =>
            descriptionKeyRef.Value.IndexErrorProtectedFormat(addition);

        public override void OnGet()
        {
            Player.data.AddStatus(new Sharp(addition, true));
        }
    }
}