using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

[Serializable]
public class BattleVertexData : VertexData
{
    public List<Enemy> enemies;

    public override VertexType Type => VertexType.Battle;
    
    public BattleVertex Init(BattleVertex prefab)
    {
        BattleVertex vertex = Object.Instantiate(prefab);
        vertex.transform.position = position;
        vertex.enemies = enemies;
        return vertex;
    }
}