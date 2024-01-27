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

        
        public override string Title => titleKeyRef.Value;

        public override string Description => descriptionKeyRef.Value;
    }
}