using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Audio;
using Battle.Config;
using Battle.Match3;
using Battle.Spells;
using Battle.Units;
using Core;
using UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Grid = Battle.Match3.Grid;

namespace Battle
{
    public class BattleManager : MonoBehaviour
    {
        public Player player;
    
        public Grid grid;
    
        private EnemyPlacer _placer;
    
        private Canvas _canvas;

        private const float FightTime = 0.2f;

        public static EnemyGroup group;

        public Enemy target;
    
        public BattleState State { get; private set; }

        public List<Enemy> enemiesPrefabs;
        public List<Enemy> enemies;

        private Coroutine _battle;

        private bool _playerActs;
        private bool _enemiesAct;
    
        private bool _dead;
    
        public void Awake()
        {
            AudioManager.instance.StopAll();
            
            _canvas = FindFirstObjectByType<Canvas>();
            player = FindFirstObjectByType<Player>();
            grid = FindFirstObjectByType<Grid>();


            enemiesPrefabs = group.GetEnemies();

            for (int i = 0; i < enemiesPrefabs.Count; i++)
            {
                enemies.Add(LoadEnemy(i));
            }
            player.TurnOn();

            AudioManager.instance.Play(AudioEnum.Battle);
        
            GameManager.instance.SaveData();
        
            BattleInterfacePlacement placement = FindFirstObjectByType<BattleInterfacePlacement>();
            placement.Place();
            
            State = BattleState.Turn;
            BattleTargetPicker.ResetPick();
            
            BattleLog.Clear();
        }

        public void OnEnable()
        {
            Grid.onEnd += EndTurn;
        }

        public void OnDisable()
        {
            Grid.onEnd -= EndTurn;
        }

        public void EndTurn()
        {
            if (State == BattleState.Turn && !player.Stunned())
            {
                StartCoroutine(PlayerAct());
            }
            else if (State != BattleState.EnemiesAct)
            {
                StartCoroutine(EnemiesAct());
            }
        }

        public IEnumerator KillEnemy(Enemy enemy)
        {
            yield return new WaitForSeconds(FightTime);
            
            enemy.Delete();
            
            if (enemies.All(v => v == null))
            {
                State = BattleState.End;
                Win();
                yield break;
            }
            BattleTargetPicker.PickNextPossible();
            yield return new WaitForSeconds(FightTime);
            
            OnEnemiesShuffle();
        }

        public IEnumerator Die()
        {
            State = BattleState.End;
        
            yield return new WaitForSeconds(FightTime);
            player.Delete();
        
            Lose();
        }

        public void ApplyConfig()
        {
            LoadSpells();
            _placer = FindFirstObjectByType<EnemyPlacer>();
            _placer.enemiesToPlace = enemies;
            _placer.Place();
            grid = FindFirstObjectByType<Grid>();
        }

        public void OnEnemiesShuffle()
        {
            _placer.enemiesToPlace = enemies;
            _placer.Place();
            BattleTargetPicker.PickNextPossible();
        }

        public void Win()
        {
            BattleEndLog.Log();
            grid.Block();
            player.Save();
            Player.data.money += group.reward;
            BattleLog.Clear();
            Log.UnAttach();
            SceneManager.LoadScene("Map");
        }

        private IEnumerator<WaitForSeconds> PlayerAct()
        {
            State = BattleState.PlayerAct;

            player.Act();
            yield return new WaitForSeconds(FightTime);

            StartCoroutine(EnemiesAct());
        }

        private IEnumerator EnemiesAct()
        {
            State = BattleState.EnemiesAct;

            foreach (var enemy in enemies.Where(enemy => enemy != null && !enemy.Stunned()))
            {
                enemy.Act();

                yield return new WaitUntil(() => grid.state == GridState.Blocked);
                yield return new WaitForSeconds(FightTime);
            
                if (player.Hp <= 0) yield break;
            }

            TurnLog.Log();
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
        private void Lose()
        {
            State = BattleState.End;
            grid.Block();
        
            GameObject menu = Instantiate(PrefabsContainer.instance.loseMessage, _canvas.transform, false);
            var buttons = menu.GetComponentsInChildren<Button>();
            buttons[0].onClick.AddListener(GameManager.instance.NewGame);
            buttons[1].onClick.AddListener(GameManager.instance.MainMenu);
            menu.gameObject.SetActive(true);
        }

        private Enemy LoadEnemy(int i)
        {
            if (enemiesPrefabs[i] == null) return null;
            Enemy enemy = Instantiate(enemiesPrefabs[i], _canvas.transform, false);
            enemy.TurnOn();
            return enemy;
        }

        private void LoadSpells()
        {
            var spells = FindFirstObjectByType<SpellsContainer>();
            var spellButtons = spells.GetComponentsInChildren<Button>();
            for (int i = 0; i < player.spells.Count && i < 4; i++)
            {
                Button btn = spellButtons[i];
                Spell spell = player.spells[i];
                btn.AddComponent<DevDebugAbleObject>();
                btn.GetComponent<DevDebugAbleObject>().text = spell.Description;
                btn.onClick.AddListener(spell.Cast);
                Text text = btn.GetComponentInChildren<Text>();
                text.text = spell.Title + " " + spell.useCost;
            }
        }
    }
}