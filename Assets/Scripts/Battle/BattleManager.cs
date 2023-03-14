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
        
        State = BattleState.Turn;

        
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
    public void EndTurn()
    {
        StartCoroutine(PlayerAct());
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
        State = BattleState.End;
        
        yield return new WaitForSeconds(fightTime);
        player.Delete();
        
        Lose();
    }

    private void Win()
    {
        grid.Block();
        SavePlayerStats();
        SceneManager.LoadScene("Map");
    }

    private IEnumerator<WaitForSeconds> PlayerAct()
    {
        if (!player.Stunned())
        {
            State = BattleState.PlayerAct;

            player.Act();
            yield return new WaitForSeconds(fightTime);
        }

        StartCoroutine(EnemiesAct());
    }

    private IEnumerator<WaitForSeconds> EnemiesAct()
    {
        State = BattleState.EnemiesAct;

        foreach (var enemy in Enemies.Where(enemy => !enemy.Stunned()))
        {
            enemy.Act();
            yield return new WaitForSeconds(fightTime);
            if (player.hp <= 0) yield break;
        }
        
        ModifierManager.Move();

        if (!player.Stunned())
        {
            State = BattleState.Turn;
            grid.Unlock();
        }
        else
        {
            StartCoroutine(EnemiesAct());
        }
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    public void Lose()
    {
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