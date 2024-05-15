using Battle.Modifiers.Statuses;
using Battle.Units;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "VampireFangs", menuName = "Items/VampireFangs")]
    public class VampireFangs : Item
    {
        [SerializeField] private int healAmount;

        public override string Title => titleKeyRef.Value;

        public override string Description => string.Format(descriptionKeyRef.Value, healAmount);

        public override void OnGet()
        {
            Player.data.AddStatus(new Vampirism(healAmount, true));
        }
    }
}