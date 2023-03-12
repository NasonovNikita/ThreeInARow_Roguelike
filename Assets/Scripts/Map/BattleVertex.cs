using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleVertex : Vertex
{
    public List<Enemy> enemies;
    
    public override void OnArrive()
    {
        BattleManager.Enemies = enemies;
        SceneManager.LoadScene("Battle");
    }
}
