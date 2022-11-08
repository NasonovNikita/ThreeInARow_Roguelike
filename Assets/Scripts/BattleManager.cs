using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private List<Enemy> enemies;

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private float attackTime;

    private void Update()
    {
        if (enemies.Count == 0)
        {
            EndOfBattle();
        }
        else if (grid.State == GridState.Blocked)
        {
            StartCoroutine(DoDamages());
        }
    }

    private IEnumerator<WaitForSeconds> DoDamages()
    {
        grid.StartUnlocking();
        yield return new WaitForSeconds(attackTime);
        enemies[0].ChangeHp(-player.Damage(grid.Destroyed));
        grid.Destroyed.Clear();
        
        foreach (Enemy enemy in enemies)
        { 
            yield return new WaitForSeconds(attackTime);
            if (enemy.Hp == 0)
            {
                Destroy(enemy.gameObject);
            }
            else
            {
                player.ChangeHp(-enemy.Damage());
            }
        }
        
        grid.Unlock();
    }

    public void KillEnemy(Enemy enemy)
    {
        enemy.Delete();
        enemies.Remove(enemy);
    }

    private void EndOfBattle()
    {
        grid.Block();
    }
}