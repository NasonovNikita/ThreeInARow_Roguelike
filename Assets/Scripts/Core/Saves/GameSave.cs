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
        [SerializeField] private int currentVertex = Map.Map.currentVertex;
        [SerializeField] private string scene = SceneManager.GetActiveScene().name;
        [SerializeField] private string playerData = JsonUtility.ToJson(Player.data);
        [SerializeField] private int seed = Globals.instance.seed;
        [SerializeField] private string enemyGroup = JsonUtility.ToJson(BattleManager.enemyGroup);
        [SerializeField] private string goods = Tools.Json.ListToJson(ShopManager.goods);
        [SerializeField] private int gridSizeX = Globals.instance.gridSize.Item1;
        [SerializeField] private int gridSizeY = Globals.instance.gridSize.Item2;

        private const string Path = "/Game.dat";

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
                goods = "{[]}"
            };

            return save;
        }
    }
}