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

        
        public override string Title => titleKeyRef.Value;

        public override string Description => string.Format(descriptionKeyRef.Value, Tools.Percents(sale),
            Tools.Percents(ShopManager.salePrice));
        
        public override void OnGet()
        {
            ShopManager.salePrice = sale;
        }
    }
}