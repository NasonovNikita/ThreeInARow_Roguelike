using System.Collections.Generic;

public static class BattleData
{
    public static List<Enemy> enemies;

    public static void Reset()
    {
        enemies = new List<Enemy>();
    }
}
