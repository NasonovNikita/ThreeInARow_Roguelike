using System;
using Battle;
using Battle.Units;
using Other;
using Shop;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Core.Saves
{
    [Serializable]
    public class GameSave : SaveObject
    {
        [SerializeField] private int currentVertex;
        [SerializeField] private string scene;
        [SerializeField] private string playerData;
        [SerializeField] private int seed;
        [SerializeField] private string enemyGroup;
        [SerializeField] private string goods;
        [SerializeField] private int gridSizeX;
        [SerializeField] private int gridSizeY;
        private bool preset;

        private const string Path = "/Game.dat";

        public GameSave()
        {
            currentVertex = Map.Map.currentVertex;
            scene = SceneManager.GetActiveScene().name;
            playerData = JsonUtility.ToJson(Player.data);
            seed = Globals.instance.seed;
            enemyGroup = JsonUtility.ToJson(BattleManager.enemyGroup);
            goods = Tools.Json.ListToJson(ShopManager.goods);
            gridSizeX = Globals.instance.gridSize.Item1;
            gridSizeY = Globals.instance.gridSize.Item2;
        }

        public static void Save()
        {
            SavesManager.Save(new GameSave(), Path);
        }

        public static void Load()
        {
            GameSave save = SavesManager.Load<GameSave>(Path);
            if (save == null) CreateEmptySave().Apply();
            else save.Apply();
        }

        public override void Apply()
        {
            Map.Map.currentVertex = currentVertex;
            
            JsonUtility.FromJsonOverwrite(playerData, Player.data);
            
            Globals.instance.seed = seed;
            Globals.instance.gridSize = (gridSizeX, gridSizeY);

            if (BattleManager.enemyGroup == null)
                BattleManager.enemyGroup = ScriptableObject.CreateInstance<EnemyGroup>();
            JsonUtility.FromJsonOverwrite(enemyGroup, BattleManager.enemyGroup);
            
            ShopManager.goods = Tools.Json.JsonToList<Good>(goods);
            Map.Map.Regenerate();
            
            SceneManager.LoadScene(scene);
        }

        public static GameSave CreateEmptySave()
        {
            GameSave save = new GameSave
            {
                currentVertex = -1,
                scene = "Map",
                playerData = JsonUtility.ToJson(
                    Object.Instantiate(Resources.Load<PlayerData>("NewGamePreset"))),
                seed = Globals.instance.randomSeed ? Random.Range(0, (int) Math.Pow(10, 6)) : Globals.instance.seed,
                enemyGroup = "{}",
                goods = "{[]}",
                preset = true
            };

            return save;
        }
    }
}