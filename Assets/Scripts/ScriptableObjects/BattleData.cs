using System.Collections.Generic;
using UnityEngine;

public static class BattleData
{
    public static List<Enemy> enemies;

    public static void Reset()
    {
        enemies = new List<Enemy>();
    }
}
