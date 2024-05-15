using Core.Singleton;
using Map.Nodes.Managers;
using UnityEngine;

namespace Map.Nodes
{
    public class BattleNode : Node
    {
        [SerializeField] private bool isBoss;

        protected override void Action()
        {
            SetNodeRandom();

            Battle.SceneManager.enemyGroup = 
                isBoss ? 
                Generator.Instance.ChooseBoss(layer) : 
                Generator.Instance.ChooseBattleEnemyGroup(layer);
            
            UnityEngine.SceneManagement.SceneManager.LoadScene("Battle");
            
            ResetRandom();
        }

        public static BattleNode Create(int layer, int randomSeed) => 
            (BattleNode)Node.Create(PrefabsContainer.instance.battleNode, layer, randomSeed);

        public static BattleNode CreateBoss(int layer, int randomSeed)
        {
            var node = (BattleNode)Node.Create(PrefabsContainer.instance.battleNode, layer, randomSeed);
            node.isBoss = true;
            return node;
        }
    }
}