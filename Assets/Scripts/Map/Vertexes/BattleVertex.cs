using Battle;
using Core;
using Core.SingletonContainers;
using Knot.Localization;
using UnityEngine.SceneManagement;

namespace Map.Vertexes
{
    public class BattleVertex : Vertex
    {
        public bool isBoss;

        protected override void OnArrive()
        {
            SetRandom();

            BattleManager.enemyGroup = isBoss ? generator.ChooseBoss(layer) : generator.ChooseBattleEnemyGroup(layer);
            
            SceneManager.LoadScene("Battle");
            
            ResetRandom();
        }

        public static BattleVertex Create(int layer, int randomSeed)
        {
            BattleVertex vertex =
                (BattleVertex)Vertex.Create(PrefabsContainer.instance.battleVertex, layer, randomSeed);
            return vertex;
        }
        
        public static BattleVertex CreateBoss(int layer, int randomSeed)
        {
            BattleVertex vertex =
                (BattleVertex)Vertex.Create(PrefabsContainer.instance.battleVertex, layer, randomSeed);
            vertex.isBoss = true;
            return vertex;
        }
    }
}