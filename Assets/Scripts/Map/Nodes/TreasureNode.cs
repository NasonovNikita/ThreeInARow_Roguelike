using Core.Singleton;

namespace Map.Nodes
{
    public class TreasureNode : Node
    {
        protected override void Action()
        {
            RoomLoader.LoadTreasure(layer, seed);
        }

        public static TreasureNode Create(int layer, int randomSeed)
        {
            return (TreasureNode)Create(PrefabsContainer.instance.treasureNode, layer,
                randomSeed);
        }
    }
}