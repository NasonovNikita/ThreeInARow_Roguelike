using Core.Singleton;

namespace Map.Nodes
{
    public class TreasureNode : Node
    {
        protected override void Action()
        {
            RoomLoader.LoadTreasure(Layer, Seed);
        }

        public static TreasureNode Create(int layer, int randomSeed) =>
            (TreasureNode)Create(PrefabsContainer.Instance.treasureNode, layer,
                randomSeed);
    }
}