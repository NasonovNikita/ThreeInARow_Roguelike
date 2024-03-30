using Core;
using Knot.Localization;
using Shop;
using UnityEngine.SceneManagement;

namespace Map.Vertexes
{
    public class ShopVertex : Vertex
    {
        protected override void OnArrive()
        {
            SetRandom();
            
            ShopManager.goods = generator.ChooseGoods(layer);
            ShopManager.entered = true;
            SceneManager.LoadScene("Shop");
            
            ResetRandom();
        }

        public static ShopVertex Create(int layer, int randomSeed)
        {
            return (ShopVertex)Vertex.Create(PrefabsContainer.instance.shopVertex, layer, randomSeed);
        }
    }
}