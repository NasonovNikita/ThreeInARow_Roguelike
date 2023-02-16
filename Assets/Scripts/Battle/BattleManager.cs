using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    public Grid grid;
    
    [SerializeField]
    private Stats playerStats;
    
    [SerializeField]
    private Canvas canvas;
    
    [SerializeField]
    private float fightTime;

    [SerializeField]
    private GameObject loseMessage;
    
    public List<Enemy> enemies;

    public Spell[] spells;

    public GameManager gameManager;
    
    public BattleState State { get; private set; }
    
    private Coroutine _battle;
    private Coroutine _playerAct;
    private Coroutine _enemiesAct;
    

    private bool _dead;
    
    private void Awake()
    {
        State = BattleState.PlayerTurn;
        
        player.grid = grid;
        player.target = enemies[0];
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
        
        LoadPlayerStats();
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
        
        if (enemies.Count == 0)
        {
            Win();
            yield break;
        }
        
        player.target = enemies[0];
    }

    public IEnumerator Die()
    {
        State = BattleState.End;
        
        yield return new WaitForSeconds(fightTime);
        player.Delete();
        
        Lose();
    }

    private void Win()
    {
        StopCoroutine(_battle);
        State = BattleState.End;
        grid.Block();
        SavePlayerStats();
        SceneManager.LoadScene("Map");
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
    
    public void Lose()
    {
        StopCoroutine(_battle);
        State = BattleState.End;
        grid.Block();
        GameObject menu = Instantiate(loseMessage, canvas.transform, false);
        Button[] buttons = menu.GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(gameManager.Restart);
        buttons[1].onClick.AddListener(gameManager.Exit);
        menu.gameObject.SetActive(true);
    }

    public void SavePlayerStats()
    {
        playerStats.playerHp = player.Hp;
        playerStats.playerMana = player.Mana;
    }

    public void LoadPlayerStats()
    {
        player.SetHp(playerStats.playerHp);
        player.SetMana(playerStats.playerMana);
    }
}