using Core.Singleton;
using Map.Nodes.Managers;
using Treasure;

namespace Map.Nodes
{
    public class TreasureNode : Node
    {
        protected override void Action()
        {
            TreasureManager.treasure = Generator.Instance.ChooseTreasure(layer);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Treasure");
        }

        public static TreasureNode Create(int layer, int randomSeed)
        {
            return (TreasureNode)Node.Create(PrefabsContainer.instance.treasureNode, layer, randomSeed);
        }
    }
}