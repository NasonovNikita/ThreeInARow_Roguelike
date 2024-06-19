using Core.Singleton;

namespace Map.Nodes
{
    public class TreasureNode : Node
    {
        protected override void Action() => RoomLoader.LoadTreasure(layer, seed);

        public static TreasureNode Create(int layer, int randomSeed) => 
            (TreasureNode)Node.Create(PrefabsContainer.instance.treasureNode, layer, randomSeed);
    }
}