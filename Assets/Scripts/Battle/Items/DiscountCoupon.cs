using Battle.Units;
using Shop;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "DiscountCoupon", menuName = "Items/DiscountCoupon")]
    public class DiscountCoupon : Item
    {
        [SerializeField] private float sale;
        public override void Use(Unit unitBelong) {}

        public override void OnBuy()
        {
            ShopManager.salePrice = sale;
        }
    }
}