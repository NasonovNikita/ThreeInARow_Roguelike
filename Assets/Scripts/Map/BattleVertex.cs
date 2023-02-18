using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BattleVertex : Vertex
{
    public BattleData battle;

    public List<Enemy> enemies;
    public override void OnArrive()
    {
        battle.enemies = enemies;
        SceneManager.LoadScene("Battle");
    }
}
