using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static Player Player;
    
    public static Grid Grid;
    
    private static Stats playerStats;
    
    private static EnemyPlacement placer;
    
    private static Canvas canvas;

    private const float FightTime = 0.2f;

    public static List<Enemy> enemies = new();

    public static Enemy target;


    public static Spell[] Spells;
    
    public static BattleState State { get; private set; }
    
    private Coroutine _battle;

    private bool _playerActs;
    private bool _enemiesAct;

    private static bool _firstBattle = true;
    

    private bool _dead;
    
    public static void TurnOn()
    {
        canvas = FindObjectOfType<Canvas>();

        playerStats = Resources.Load<Stats>("RuntimeData/PlayerStats");

        placer = FindObjectOfType<EnemyPlacement>();
        
        if (_firstBattle)
        {
            _firstBattle = false;
            LoadPlayerStats();
        }
        
        State = BattleState.Turn;

        
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i] = LoadEnemy(i);
        }
        
        Player.grid = Grid;
        target = enemies[0];

        Debug.Log(enemies.Count);
        placer.enemiesToPlace = enemies;
        placer.Place();
        
        foreach (Enemy enemy in enemies)
        {
            enemy.player = Player;
        }
        
        LoadPlayerStats();
        
        GameManager.SaveData();
    }
    public void EndTurn()
    {
        StartCoroutine(PlayerAct());
    }

    public static IEnumerator KillEnemy(Enemy enemy)
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

    public static IEnumerator Die()
    {
        State = BattleState.End;
        
        yield return new WaitForSeconds(FightTime);
        Player.Delete();
        
        Lose();
    }

    private static void Win()
    {
        Grid.Block();
        SavePlayerStats();
        SceneManager.LoadScene("Map");
    }

    private IEnumerator<WaitForSeconds> PlayerAct()
    {
        if (!Player.Stunned())
        {
            State = BattleState.PlayerAct;

            Player.Act();
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
            if (Player.hp <= 0) yield break;
        }
        
        ModifierManager.Move();

        if (!Player.Stunned())
        {
            State = BattleState.Turn;
            Grid.destroyed.Clear();
            Grid.Unlock();
        }
        else
        {
            StartCoroutine(EnemiesAct());
        }
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    public static void Lose()
    {
        State = BattleState.End;
        Grid.Block();
        GameObject menu = Instantiate(Resources.Load<GameObject>("Prefabs/Menu/Lose"), canvas.transform, false);
        Button[] buttons = menu.GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(GameManager.Restart);
        buttons[1].onClick.AddListener(GameManager.Exit);
        menu.gameObject.SetActive(true);
    }

    public static void SavePlayerStats()
    {
        playerStats.playerHp = Player.hp;
        playerStats.playerMana = Player.mana;
    }

    public static void LoadPlayerStats()
    {
        Player.hp = playerStats.playerHp;
        Player.mana = playerStats.playerMana;
        Player.damage = playerStats.playerDamage;
        Player.manaPerGem = playerStats.manaPerGem;
    }

    private static Enemy LoadEnemy(int i)
    {
        return Instantiate(enemies[i], canvas.transform, false);
    }
}