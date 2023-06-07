using System;
using System.Collections.Generic;
using Shop;
using Object = UnityEngine.Object;

namespace Map.Vertexes
{
    [Serializable]
    public class ShopVertexData : VertexData
    { 
        public List<Good> goods;

        public override VertexType Type => VertexType.Shop;

        public ShopVertex Init(ShopVertex prefab)
        {
            ShopVertex vertex = Object.Instantiate(prefab);
            vertex.transform.position = position;
            vertex.goods = goods;
            return vertex;
        }

    }
}