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


    public Spell[] Spells;
    
    public static BattleState State { get; private set; }
    
    private Coroutine _battle;

    private bool _playerActs;
    private bool _enemiesAct;

    private bool _firstBattle = true;
    

    private bool _dead;
    
    public void TurnOn()
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
        
        Player.grid = Grid;
        Player.target = Enemies[0];
        Player.manager = this;

        placer.enemiesToPlace = Enemies;
        placer.Place();
        
        foreach (Enemy enemy in Enemies)
        {
            enemy.player = Player;
            enemy.manager = this;
        }

        foreach (Spell spell in Spells)
        {
            spell.Player = Player;
            spell.Manager = this;
        }
        
        Grid.manager = this;
        
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

        Player.target = Enemies[0];

        yield return new WaitForSeconds(fightTime);
        
        enemy.Delete();
    }

    public IEnumerator Die()
    {
        State = BattleState.End;
        
        yield return new WaitForSeconds(fightTime);
        Player.Delete();
        
        Lose();
    }

    private void Win()
    {
        Grid.Block();
        SavePlayerStats();
        Map.currentVertex = Map.nextVertex;
        SceneManager.LoadScene("Map");
    }

    private IEnumerator<WaitForSeconds> PlayerAct()
    {
        if (!Player.Stunned())
        {
            State = BattleState.PlayerAct;

            Player.Act();
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
            if (Player.hp <= 0) yield break;
        }
        
        ModifierManager.Move();

        if (!Player.Stunned())
        {
            State = BattleState.Turn;
            Grid.Unlock();
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
        Grid.Block();
        GameObject menu = Instantiate(loseMessage, canvas.transform, false);
        Button[] buttons = menu.GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(GameManager.Restart);
        buttons[1].onClick.AddListener(GameManager.Exit);
        menu.gameObject.SetActive(true);
    }

    public void SavePlayerStats()
    {
        playerStats.playerHp = Player.hp;
        playerStats.playerMana = Player.mana;
    }

    public void LoadPlayerStats()
    {
        Player.hp = playerStats.playerHp;
        Player.mana = playerStats.playerMana;
        Player.damage = playerStats.playerDamage;
    }
}