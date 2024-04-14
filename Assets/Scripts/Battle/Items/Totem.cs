using Battle.Modifiers.Statuses;
using Battle.Units;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "Totem", menuName = "Items/Totem")]
    public class Totem : Item
    {
        [SerializeField] private int chance;

        public override string Title => titleKeyRef.Value;
        public override string Description => descriptionKeyRef.Value;


        public override void Get()
        {
            Player.data.AddStatus(new Immortality(chance, true));
            
            base.Get();
        }
    }
}