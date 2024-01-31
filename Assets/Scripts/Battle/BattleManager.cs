using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Audio;
using Battle.Config;
using Battle.Match3;
using Battle.Units;
using Core;
using Core.Saves;
using UI.Battle;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Grid = Battle.Match3.Grid;

namespace Battle
{
    public class BattleManager : MonoBehaviour
    {
        [NonSerialized] public Player player;
    
        private Grid grid;
    
        private EnemyPlacer placer;
    
        [SerializeField] private Canvas canvas;
        [SerializeField] private TurnLabel turnHUD;

        private const float FightTime = 0.2f;

        public static EnemyGroup enemyGroup;

        public Enemy target;
    
        public BattleState State { get; private set; }

        private List<Enemy> enemiesPrefabs;
        public List<Enemy> enemies;

        private Coroutine battle;

        private bool playerActs;
        private bool enemiesAct;
    
        private bool dead;
    
        public void Awake()
        {
            AudioManager.instance.StopAll();
            
            player = FindFirstObjectByType<Player>();
            grid = FindFirstObjectByType<Grid>();


            enemiesPrefabs = enemyGroup.GetEnemies();

            for (int i = 0; i < enemiesPrefabs.Count; i++)
            {
                enemies.Add(LoadEnemy(i));
            }
            player.TurnOn();

            AudioManager.instance.Play(AudioEnum.Battle);
        
            SavesManager.SaveGame();
        
            BattleInterfacePlacement placement = FindFirstObjectByType<BattleInterfacePlacement>();
            placement.Place();
            
            State = BattleState.Turn;
            turnHUD.SetPlayerTurn();
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
            placer = FindFirstObjectByType<EnemyPlacer>();
            placer.enemiesToPlace = enemies;
            placer.Place();
            grid = FindFirstObjectByType<Grid>();
        }

        public void OnEnemiesShuffle()
        {
            placer.enemiesToPlace = enemies;
            placer.Place();
            BattleTargetPicker.PickNextPossible();
        }

        public void Win()
        {
            BattleEndLog.Log();
            grid.Block();
            player.Save();
            Player.data.money += enemyGroup.reward;
            BattleLog.Clear();
            Log.UnAttach();
            SceneManager.LoadScene("Map");
        }

        private IEnumerator<WaitForSeconds> PlayerAct()
        {
            State = BattleState.PlayerAct;

            player.Act();
            yield return new WaitForSeconds(FightTime);

            turnHUD.SetEnemyTurn();
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
            
                if (player.hp <= 0) yield break;
            }

            TurnLog.Log();
            if (!player.Stunned())
            {
                turnHUD.SetPlayerTurn();
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
        
            GameObject menu = Instantiate(PrefabsContainer.instance.loseMessage, canvas.transform);
            var buttons = menu.GetComponentsInChildren<Button>();
            buttons[0].onClick.AddListener(GameManager.instance.NewGame);
            buttons[1].onClick.AddListener(GameManager.instance.MainMenu);
            menu.gameObject.SetActive(true);
        }

        private Enemy LoadEnemy(int i)
        {
            if (enemiesPrefabs[i] == null) return null;
            Enemy enemy = Instantiate(enemiesPrefabs[i], canvas.transform, false);
            enemy.TurnOn();
            return enemy;
        }
    }
}