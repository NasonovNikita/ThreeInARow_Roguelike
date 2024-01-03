using Battle.Units;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "EternalIce", menuName = "Items/EternalIce")]
    public class EternalIce : Item
    {
        public override void Use(Unit unitBelong)
        {
            unitBelong.canFullyFreeze = true;
        }
    }
}