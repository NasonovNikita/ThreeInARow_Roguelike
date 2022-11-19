using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    public List<Enemy> enemies;

    [SerializeField]
    private Grid grid;

    [SerializeField]
    private float fightTime;

    private BattleState _state;
    public BattleState State => _state;

    public Spell[] spells;
    private void Awake()
    {
        _state = BattleState.PlayerTurn;
        
        player.grid = grid;
        player.enemies = enemies;
        
        foreach (Enemy enemy in enemies)
        {
            enemy.player = player;
            enemy.manager = this;
        }

        foreach (Spell spell in spells)
        {
            spell.player = player;
            spell.manager = this;
        }
        
        grid.manager = this;
    }

    private void Update()
    {
        if (enemies.Count == 0)
        {
            EndOfBattle();
        }
    }

    private IEnumerator<WaitForSeconds> Battle()
    {
        if (!player.Stunned())
        {
            PlayerAct();
            yield return new WaitForSeconds(fightTime);
        }
        
        EnemiesAct();
        
        yield return new WaitForSeconds(fightTime);

        if (!player.Stunned())
        {
            _state = BattleState.PlayerTurn;
        
            grid.Unlock();
        }
        else
        {
            player.Move();
            
            if (player.Stunned())
            {
                StartCoroutine(Battle());
            }
            else
            {
                _state = BattleState.PlayerTurn;
        
                grid.Unlock();
            }
        }
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
        StartCoroutine(Battle());
    }

    private void PlayerAct()
    {
        _state = BattleState.PlayerAct;

        player.Act();
    }

    private void EnemiesAct()
    {
        _state = BattleState.EnemiesAct;
        
        foreach (Enemy enemy in enemies)
        {
            if (!enemy.Stunned())
            {
                enemy.Act();
            }
            else
            {
                enemy.Move();
            }
        }
    }
}