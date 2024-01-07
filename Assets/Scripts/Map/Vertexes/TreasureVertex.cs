using Core;
using Other;
using Treasure;
using UnityEngine.SceneManagement;

namespace Map.Vertexes
{
    public class TreasureVertex : Vertex
    {
        public GetAble treasure;
        public override void OnArrive()
        {
            TreasureManager.treasure = treasure;
            SceneManager.LoadScene("Treasure");
        }

        public static TreasureVertex Create()
        {
            return (TreasureVertex)Vertex.Create(PrefabsContainer.instance.treasureVertex);
        }
    }
}