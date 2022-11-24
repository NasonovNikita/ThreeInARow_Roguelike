using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private Grid grid;
    
    [SerializeField]
    private float fightTime;

    public List<Enemy> enemies;

    public Spell[] spells;
    
    public BattleState State { get; private set; }
    
    private Coroutine _battle;
    private Coroutine _playerAct;
    private Coroutine _enemiesAct;
    

    private bool _dead;
    
    private void Awake()
    {
        State = BattleState.PlayerTurn;
        
        player.grid = grid;
        player.enemies = enemies;
        player.manager = this;
        
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
        if (State == BattleState.End) return;
        
        if (enemies.Count == 0)
        {
            Win();
        }

        if (_dead)
        {
            Lose();
        }
    }
    
    private IEnumerator Battle()
    {
        if (!player.Stunned())
        {
            _playerAct = StartCoroutine(PlayerAct());
        }

        yield return new WaitUntil(() => _playerAct == null);
        
        StartCoroutine(EnemiesAct());
        
        yield return new WaitUntil(() => _enemiesAct == null);
        
        Move();
        
        if (!player.Stunned())
        {
            State = BattleState.PlayerTurn;
            grid.Unlock();
        }
        else
        {
            _battle = StartCoroutine(Battle());
        }
    }

    public void EndTurn()
    {
        _battle = StartCoroutine(Battle());
    }

    public IEnumerator KillEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        yield return new WaitForSeconds(fightTime);
        enemy.Delete();
    }

    public IEnumerator Die()
    {
        _dead = true;
        yield return new WaitForSeconds(fightTime);
        player.Delete();
    }

    private void Win()
    {
        State = BattleState.End;
        grid.Block();
    }

    private void Lose()
    {
        StopCoroutine(_battle);
        State = BattleState.End;
        grid.Block();
    }

    private IEnumerator PlayerAct()
    {
        State = BattleState.PlayerAct;
        yield return new WaitForSeconds(fightTime);
        player.Act();
        
        _playerAct = null;
    }

    private IEnumerator EnemiesAct()
    {
        State = BattleState.EnemiesAct;

        foreach (var enemy in enemies.Where(enemy => !enemy.Stunned()))
        {
            yield return new WaitForSeconds(fightTime);
            enemy.Act();
        }

        _enemiesAct = null;
    }

    private void Move()
    {
        player.Move();

        foreach (Enemy enemy in enemies)
        {
            enemy.Move();
        }
    }
}