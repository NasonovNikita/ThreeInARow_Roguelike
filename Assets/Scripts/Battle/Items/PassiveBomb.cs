using Battle.Units;
using Other;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "PassiveBomb", menuName = "Items/PassiveBomb")]
    public class PassiveBomb : Item
    {
        [SerializeField] private int minMana;
        [SerializeField] private int damage;

        public override string Description =>
            descriptionKeyRef.Value.IndexErrorProtectedFormat(minMana, damage);

        public override void OnGet()
        {
            Player.Data.AddStatus(new Units.Statuses.PassiveBomb(damage, minMana, true));
        }
    }
}