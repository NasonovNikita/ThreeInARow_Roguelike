using Core.Singleton;

namespace Map.Nodes
{
    public class ShopNode : Node
    {
        protected override void Action() => RoomLoader.LoadShop(layer, seed);

        public static ShopNode Create(int layer, int seed) =>
            (ShopNode)Node.Create(PrefabsContainer.instance.shopNode, layer, seed);
    }
}