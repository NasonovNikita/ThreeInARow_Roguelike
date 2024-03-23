using Other;
using Shop;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "DiscountCoupon", menuName = "Items/DiscountCoupon")]
    public class DiscountCoupon : Item
    {
        [SerializeField] private float sale;
        
        public override string Title => titleKeyRef.Value;

        public override string Description => string.Format(descriptionKeyRef.Value, Tools.Percents(sale),
            Tools.Percents(ShopManager.salePrice));

        public override void Get()
        {
            ShopManager.salePrice = sale;
            base.Get();
        }
    }
}