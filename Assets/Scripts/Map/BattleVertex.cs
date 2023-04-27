using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BattleVertex : Vertex
{
    public List<string> enemies;
    
    public override void OnArrive()
    {
        BattleManager.enemiesNames = enemies;
        SceneManager.LoadScene("Battle");
    }
}
