using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BattleVertex : Vertex
{
    public List<Enemy> enemies;
    
    public override void OnArrive()
    {
        BattleManager.enemies = new List<Enemy>(enemies);
        SceneManager.LoadScene("Battle");
    }
}
