using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle Data", fileName = "BattleData", order = 52)]
public static class BattleData
{
    public static List<Enemy> enemies;

    public static void Reset()
    {
        enemies = new List<Enemy>();
    }
}
