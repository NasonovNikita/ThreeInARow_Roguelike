using Battle.Units;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "PassiveBomb", menuName = "Items/PassiveBomb")]
    public class PassiveBomb : Item
    {
        [SerializeField] private int minMana;
        [SerializeField] private int damage;

        
        public override string Title => titleKeyRef.Value;

        public override string Description => string.Format(descriptionKeyRef.Value, minMana, damage);

        public override void Get()
        {
            Player.data.AddStatus(new Modifiers.Statuses.PassiveBomb(damage, minMana, true));
            base.Get();
        }
    }
}