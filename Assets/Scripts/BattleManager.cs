using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private Enemy[] enemies;

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private float attackTime;

    private void Update()
    {
        if (grid.State == GridState.Blocked)
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
            player.ChangeHp(-enemy.Damage());
        }
        
        grid.Unlock();
    }
}
