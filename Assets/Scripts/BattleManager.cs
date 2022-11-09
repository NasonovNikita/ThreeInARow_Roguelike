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
    private float fightTime;

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
        
        yield return new WaitForSeconds(fightTime);
        
        player.ChangeMana(player.CountMana(grid.destroyed));
        enemies[0].ChangeHp(-player.Damage(grid.destroyed));
        grid.destroyed.Clear();
        
        foreach (Enemy enemy in enemies)
        {
            player.ChangeHp(-enemy.Damage());
            
            yield return new WaitForSeconds(fightTime);
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