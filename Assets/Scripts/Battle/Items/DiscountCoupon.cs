using Battle.Units;
using Other;
using Shop;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "DiscountCoupon", menuName = "Items/DiscountCoupon")]
    public class DiscountCoupon : Item
    {
        [SerializeField] private float sale;
        public override void Use(Unit unitBelong) {}

        public override string Title => "Discount Coupon";

        public override string Description =>
            $"A good (on sale) in the shop costs now {Tools.Percents(sale)}% instead of {Tools.Percents(ShopManager.salePrice)}%";

        public override void OnGet()
        {
            ShopManager.salePrice = sale;
        }
    }
}