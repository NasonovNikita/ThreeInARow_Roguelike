using System.Collections;
using System.Collections.Generic;
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
    
    public static List<Enemy> Enemies;


    public Spell[] spells;

    public GameManager gameManager;
    
    public BattleState State { get; private set; }
    
    private Coroutine _battle;

    private bool _playerActs;
    private bool _enemiesAct;

    private bool _firstBattle = true;
    

    private bool _dead;
    
    private void Awake()
    {
        if (_firstBattle)
        {
            _firstBattle = false;
            LoadPlayerStats();
        }
        
        State = BattleState.PlayerTurn;

        
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i] = Instantiate(Enemies[i], canvas.transform, false);
        }
        
        player.grid = grid;
        player.target = Enemies[0];
        player.manager = this;

        placer.enemiesToPlace = Enemies;
        placer.Place();
        
        foreach (Enemy enemy in Enemies)
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

        ModifierManager.Move();

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
        Enemies.Remove(enemy);
        
        if (Enemies.Count == 0)
        {
            State = BattleState.End;
            yield return new WaitForSeconds(fightTime);
            enemy.Delete();
            Win();
            yield break;
        }

        player.target = Enemies[0];

        yield return new WaitForSeconds(fightTime);
        
        enemy.Delete();
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
        grid.Block();
        SavePlayerStats();
        SceneManager.LoadScene("Map");
    }

    private IEnumerator<WaitForSeconds> PlayerAct()
    {
        _playerActs = true;
        State = BattleState.PlayerAct;
        
        player.Act();
        yield return new WaitForSeconds(fightTime);

        _playerActs = false;
    }

    private IEnumerator<WaitForSeconds> EnemiesAct()
    {
        _enemiesAct = true;
        State = BattleState.EnemiesAct;

        foreach (var enemy in Enemies)
        {
            enemy.Act();
            yield return new WaitForSeconds(fightTime);
            if (player.hp <= 0) yield break;
        }

        _enemiesAct = false;
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
        playerStats.playerHp = player.hp;
        playerStats.playerMana = player.mana;
    }

    public void LoadPlayerStats()
    {
        player.hp = playerStats.playerHp;
        player.mana = playerStats.playerMana;
        player.damage = playerStats.playerDamage;
    }
}