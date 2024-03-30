using Battle.Modifiers.Statuses;
using Battle.Units;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(menuName = "Items/Mace")]
    public class Mace : Item
    {
        [SerializeField] private int addition;


        public override string Title => titleKeyRef.Value;

        public override string Description =>
            string.Format(descriptionKeyRef.Value, addition);

        public override void Get()
        {
            Player.data.AddStatus(new Sharp(addition, true));
            
            base.Get();
        }
    }
}