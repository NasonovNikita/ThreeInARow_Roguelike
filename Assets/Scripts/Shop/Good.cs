using Battle.Units;
using Other;
using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(fileName = "Good", menuName = "Good")]
    public class Good : GetAble
    {
        public int price;

        public void Buy()
        {
            if (Player.data.money < price) return;
            Player.data.money -= price;
            Get();
        }
    }
}