using Core.Singleton;
using UnityEngine;

namespace Map.Nodes
{
    public class BattleNode : Node
    {
        [SerializeField] private bool isBoss;

        protected override void Action()
        {
            RoomLoader.LoadBattle(layer, seed, isBoss);
        }

        public static BattleNode Create(int layer, int seed, bool isBoss)
        {
            var node = (BattleNode)Create(PrefabsContainer.instance.battleNode, layer, seed);
            node.isBoss = isBoss;

            return node;
        }
    }
}