using Battle.Units;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "ManaVessel", menuName = "Items/ManaVessel")]
    public class ManaVessel : Item
    {
        [SerializeField] private int addToBorder;
        [SerializeField] private int addValue;
        
        public override void Use(Unit unitBelong) {}

        public override void OnGet()
        {
            Player.data.mana.ChangeBorderUp(addToBorder, addValue);
        }
    }
}