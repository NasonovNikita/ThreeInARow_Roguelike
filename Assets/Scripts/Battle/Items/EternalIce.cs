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

        public override string Title => "Eternal Ice";

        public override string Description => "Cold can totally freeze enemies (stun)";
    }
}