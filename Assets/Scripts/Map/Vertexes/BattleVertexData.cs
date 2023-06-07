using System;
using Battle.Units;
using Object = UnityEngine.Object;

namespace Map.Vertexes
{
    [Serializable]
    public class BattleVertexData : VertexData
    {
        public EnemyGroup group;

        public override VertexType Type => VertexType.Battle;
    
        public BattleVertex Init(BattleVertex prefab)
        {
            BattleVertex vertex = Object.Instantiate(prefab);
            vertex.transform.position = position;
            vertex.group = group;
            return vertex;
        }
    }
}