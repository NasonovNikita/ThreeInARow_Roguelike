using Core.Singleton;

namespace Map.Nodes
{
    public class ShopNode : Node
    {
        protected override void Action()
        {
            RoomLoader.LoadShop(Layer, Seed);
        }

        public static ShopNode Create(int layer, int seed)
        {
            return (ShopNode)Create(PrefabsContainer.Instance.shopNode, layer, seed);
        }
    }
}