using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle Data", fileName = "BattleData", order = 52)]
public class BattleData : ScriptableObject
{
    public List<Enemy> enemies;

    public void Reset()
    {
        enemies = new List<Enemy>();
    }
}
