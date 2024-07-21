using Battle.Units;
using Battle.Units.Statuses;
using Other;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "VampireFangs", menuName = "Items/VampireFangs")]
    public class VampireFangs : Item
    {
        [SerializeField] private int healAmount;

        public override string Description =>
            descriptionKeyRef.Value.IndexErrorProtectedFormat(healAmount);

        public override void OnGet()
        {
            Player.data.AddStatus(new Vampirism(healAmount, true));
        }
    }
}