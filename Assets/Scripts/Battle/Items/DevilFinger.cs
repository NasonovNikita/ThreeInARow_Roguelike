using Battle.Modifiers.Statuses;
using Battle.Units;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(menuName = "Items/DevilFinger")]
    public class DevilFinger : Item
    {
        [SerializeField] private int value;
        
        public override string Title => titleKeyRef.Value;
        public override string Description => string.Format(descriptionKeyRef.Value, value);

        public override void Get()
        {
            Player.data.AddStatus(new Deal(value, true));
            
            base.Get();
        }
    }
}