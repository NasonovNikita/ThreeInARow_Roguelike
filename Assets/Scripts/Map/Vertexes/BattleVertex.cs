using Battle;
using UnityEngine.SceneManagement;

public class BattleVertex : Vertex
{
    public EnemyGroup group;
    
    public override void OnArrive()
    {
        BattleManager.group = group;
        SceneManager.LoadScene("Battle");
    }
}