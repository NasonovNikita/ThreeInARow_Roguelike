using Core;
using Treasure;
using UnityEngine.SceneManagement;

namespace Map.Vertexes
{
    public class TreasureVertex : Vertex
    {
        protected override void OnArrive()
        {
            TreasureManager.treasure = generator.ChooseTreasure(layer);
            SceneManager.LoadScene("Treasure");
        }

        public static TreasureVertex Create(int layer, int randomSeed)
        {
            return (TreasureVertex)Vertex.Create(PrefabsContainer.instance.treasureVertex, layer, randomSeed);
        }
    }
}