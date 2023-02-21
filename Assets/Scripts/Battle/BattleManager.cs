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
    private EnemyPlacement placer;
    
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

    private bool _playerActs;
    private bool _enemiesAct;
    

    private bool _dead;
    
    private void Awake()
    {
        State = BattleState.PlayerTurn;

        enemies = BattleData.enemies;

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i] = Instantiate(enemies[i], canvas.transform, false);
        }
        
        player.grid = grid;
        player.target = enemies[0];
        player.enemies = enemies;
        player.manager = this;

        placer.enemiesToPlace = enemies;
        placer.Place();
        
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
        StartCoroutine(PlayerAct());

        yield return new WaitUntil(() => !_playerActs);
        
        StartCoroutine(EnemiesAct());
        
        yield return new WaitUntil(() => !_enemiesAct);

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
        if (_battle != null) StopCoroutine(_battle);
        
        State = BattleState.End;
        
        yield return new WaitForSeconds(fightTime);
        player.Delete();
        
        Lose();
    }

    private void Win()
    {
        if (_battle != null) StopCoroutine(_battle);
        State = BattleState.End;
        grid.Block();
        SavePlayerStats();
        SceneManager.LoadScene("Map");
    }

    private IEnumerator<WaitUntil> PlayerAct()
    {
        _playerActs = true;
        State = BattleState.PlayerAct;
        
        StartCoroutine(player.Act(fightTime));
        yield return new WaitUntil(() => !player.acts);

        _playerActs = false;
    }

    private IEnumerator<WaitUntil> EnemiesAct()
    {
        _enemiesAct = true;
        State = BattleState.EnemiesAct;

        foreach (var enemy in enemies)
        {
            StartCoroutine(enemy.Act(fightTime));
            yield return new WaitUntil(() => !enemy.acts);
            if (player.Hp <= 0) yield break;
        }

        _enemiesAct = false;
    }

    private void Move()
    {
        player.Move();

        foreach (Enemy enemy in enemies)
        {
            enemy.Move();
        }
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    public void Lose()
    {
        if (_battle != null) StopCoroutine(_battle);
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