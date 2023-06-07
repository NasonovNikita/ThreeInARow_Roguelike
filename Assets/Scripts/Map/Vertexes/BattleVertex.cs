using Battle;
using Battle.Units;
using UnityEngine.SceneManagement;

namespace Map.Vertexes
{
    public class BattleVertex : Vertex
    {
        public EnemyGroup group;

        protected override void OnArrive()
        {
            BattleManager.group = group;
            SceneManager.LoadScene("Battle");
        }
    }
}