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

    private BattleState _state;

    private void Start()
    {
        _state = BattleState.PlayerTurn;
        grid.manager = this;
    }

    private void Update()
    {
        if (enemies.Count == 0)
        {
            EndOfBattle();
        }
        else if (_state == BattleState.ToProcess)
        {
            StartCoroutine(Battle());
        }
    }

    private IEnumerator<WaitForSeconds> Battle()
    {
        _state = BattleState.Processing;
        
        yield return new WaitForSeconds(fightTime);
        
        player.ChangeMana(player.CountMana(grid.destroyed));
        enemies[0].ChangeHp(-player.Damage(grid.destroyed));
        grid.destroyed.Clear();

        _state = BattleState.EnemiesTurn;
        foreach (Enemy enemy in enemies)
        {
            player.ChangeHp(-enemy.Damage());
            
            yield return new WaitForSeconds(fightTime);
        }

        _state = BattleState.PlayerTurn;
        grid.Unlock();
    }

    public IEnumerator<WaitForSeconds> KillEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        yield return new WaitForSeconds(fightTime);
        enemy.Delete();
    }

    private void EndOfBattle()
    {
        _state = BattleState.End;
        grid.Block();
    }

    public void EndTurn()
    {
        _state = BattleState.ToProcess;
    }
}