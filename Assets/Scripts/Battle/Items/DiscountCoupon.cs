using Other;
using UnityEngine;

namespace Battle.Items
{
    [CreateAssetMenu(fileName = "DiscountCoupon", menuName = "Items/DiscountCoupon")]
    public class DiscountCoupon : Item
    {
        [SerializeField] private float sale;

        public override string Description =>
            descriptionKeyRef.Value.IndexErrorProtectedFormat(
                Tools.Percents(sale),
                Tools.Percents(Shop.SceneManager.SalePrice));

        public override void OnGet()
        {
            Shop.SceneManager.SalePrice = sale;
        }
    }
}