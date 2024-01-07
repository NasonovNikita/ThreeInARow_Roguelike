using Battle;
using Battle.Units;
using Core;
using UnityEngine.SceneManagement;

namespace Map.Vertexes
{
    public class BattleVertex : Vertex
    {
        public EnemyGroup group;
    
        public override void OnArrive()
        {
            BattleManager.group = group;
            SceneManager.LoadScene("Battle");
        }

        public static BattleVertex Create()
        {
            return (BattleVertex) Vertex.Create(PrefabsContainer.instance.battleVertex);
        }
    }
}