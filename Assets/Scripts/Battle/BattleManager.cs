using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public Player player;
    
    public Grid grid;
    
    private Stats playerStats;
    
    private EnemyPlacement placer;
    
    private Canvas canvas;

    private const float FightTime = 0.2f;

    public static List<Enemy> enemies = new();

    public Enemy target;
    
    public BattleState State { get; private set; }
    
    private Coroutine _battle;

    private bool _playerActs;
    private bool _enemiesAct;
    
    private bool _dead;
    
    public void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
        player = FindObjectOfType<Player>();
        grid = FindObjectOfType<Grid>();
        playerStats = Resources.Load<Stats>("RuntimeData/PlayerStats");
        placer = FindObjectOfType<EnemyPlacement>();
        
        State = BattleState.Turn;
        
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i] = LoadEnemy(i);
        }

        placer.enemiesToPlace = enemies;
        
        player.grid = grid;
        target = enemies[0];

        placer.Place();
        
        LoadPlayerStats();
        
        GameManager.instance.SaveData();
    }
    public void EndTurn()
    {
        StartCoroutine(PlayerAct());
    }

    public IEnumerator KillEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        
        if (enemies.Count == 0)
        {
            State = BattleState.End;
            yield return new WaitForSeconds(FightTime);
            enemy.Delete();
            Win();
            yield break;
        }

        target = enemies[0];

        yield return new WaitForSeconds(FightTime);
        
        enemy.Delete();
    }

    public IEnumerator Die()
    {
        State = BattleState.End;
        
        yield return new WaitForSeconds(FightTime);
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
            yield return new WaitForSeconds(FightTime);
        }

        StartCoroutine(EnemiesAct());
    }

    private IEnumerator<WaitForSeconds> EnemiesAct()
    {
        State = BattleState.EnemiesAct;

        foreach (var enemy in enemies.Where(enemy => !enemy.Stunned()))
        {
            enemy.Act();
            yield return new WaitForSeconds(FightTime);
            if (player.hp <= 0) yield break;
        }
        
        ModifierManager.Move();

        if (!player.Stunned())
        {
            State = BattleState.Turn;
            grid.destroyed.Clear();
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
        GameObject menu = Instantiate(Resources.Load<GameObject>("Prefabs/Menu/Lose"), canvas.transform, false);
        Button[] buttons = menu.GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(GameManager.instance.NewGame);
        buttons[1].onClick.AddListener(GameManager.instance.MainMenu);
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
        player.manaPerGem = playerStats.manaPerGem;
    }

    private Enemy LoadEnemy(int i)
    {
        return Instantiate(enemies[i], canvas.transform, false);
    }
}