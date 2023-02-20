using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BattleVertex : Vertex
{
    public List<Enemy> enemies;
    public override void OnArrive()
    {
        BattleData.enemies = enemies;
        SceneManager.LoadScene("Battle");
    }
}
