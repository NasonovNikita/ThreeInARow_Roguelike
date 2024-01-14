using System.Collections.Generic;
using Core;
using Shop;
using UnityEngine.SceneManagement;

namespace Map.Vertexes
{
    public class ShopVertex : Vertex
    {
        public List<Good> goods;
    
        public override void OnArrive()
        {
            ShopManager.goods = goods;
            ShopManager.entered = true;
            SceneManager.LoadScene("Shop");
        }

        public static ShopVertex Create()
        {
            return (ShopVertex)Vertex.Create(PrefabsContainer.instance.shopVertex);
        }
    }
}