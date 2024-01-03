using System.Collections.Generic;
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
            SceneManager.LoadScene("Shop");
        }
    }
}