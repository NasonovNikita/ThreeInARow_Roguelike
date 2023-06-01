using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public Player player;
    
    public Grid grid;
    
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

    private GameObject loseMessage;
    
    public void Awake()
    {
        AudioManager.instance.StopAll();
        
        loseMessage = Resources.Load<GameObject>("Prefabs/Menu/Lose");
        
        canvas = FindFirstObjectByType<Canvas>();
        player = FindFirstObjectByType<Player>();
        grid = FindFirstObjectByType<Grid>();
        placer = FindFirstObjectByType<EnemyPlacement>();

        player.Load();
        player.TurnOn();

        LoadSpells();
        
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i] = LoadEnemy(i);
        }

        placer.enemiesToPlace = enemies;
        
        placer.Place();
        
        target = enemies[0];
        
        GameManager.instance.SaveData();
        
        AudioManager.instance.Play(AudioEnum.Battle);
        
        State = BattleState.Turn;
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
        player.Save();
        Player.data.money += 10;
        Modifier.mods.Clear();
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
        
        Modifier.Move();

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
    private void Lose()
    {
        State = BattleState.End;
        grid.Block();
        
        GameObject menu = Instantiate(loseMessage, canvas.transform, false);
        Button[] buttons = menu.GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(GameManager.instance.NewGame);
        buttons[1].onClick.AddListener(GameManager.instance.MainMenu);
        menu.gameObject.SetActive(true);
    }

    private Enemy LoadEnemy(int i)
    {
        Enemy enemy = Instantiate(enemies[i], canvas.transform, false);
        enemy.TurnOn();
        return enemy;
    }

    private void LoadSpells()
    {
        var spells = GameObject.Find("Spells");
        var spellButtons = spells.GetComponentsInChildren<Button>();
        for (int i = 0; i < player.spells.Count && i < 4; i++)
        {
            Button btn = spellButtons[i];
            Spell spell = player.spells[i];
            btn.onClick.AddListener(spell.Cast);
            TMP_Text text = btn.GetComponentInChildren<TMP_Text>();
            text.text = spell.title;
        }
    }
}