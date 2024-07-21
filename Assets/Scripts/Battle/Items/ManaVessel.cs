using Battle.Units;
using Other;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "ManaVessel", menuName = "Items/ManaVessel")]
    public class ManaVessel : Item
    {
        [SerializeField] private int addToBorder;
        [SerializeField] private int addValue;

        public override string Description =>
            descriptionKeyRef.Value.IndexErrorProtectedFormat(addToBorder, addValue);

        public override void OnGet()
        {
            Player.data.mana.ChangeBorderUp(addToBorder, addValue);
        }
    }
}