using System;
using Battle.Items;
using Battle.Units;
using Map.Nodes.Managers;
using Other;
using Shop;
using Treasure;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Core.Saves
{
    [Serializable]
    public class GameSave : SaveObject
    {
        private const string Path = "/Game.dat";

        private const string NullObject = "{}";
        private const string EmptyList = "{[]}";
        [SerializeField] private int currentVertex = NodeController.Instance.currentNodeIndex;
        [SerializeField] private bool noNodeIsChosen = NodeController.Instance.noNodeIsChosen;
        [SerializeField] private string scene = SceneManager.GetActiveScene().name;
        [SerializeField] private string playerData = JsonUtility.ToJson(Player.Data);
        [SerializeField] private int seed = Globals.Instance.seed;

        [SerializeField]
        private string enemyGroup = JsonUtility.ToJson(Battle.SceneManager.enemyGroup);

        [SerializeField] private string goods = Tools.Json.ListToJson(ShopManager.goods);
        [SerializeField] private string treasure = JsonUtility.ToJson(TreasureManager.treasure);
        [SerializeField] private int gridSizeX = Globals.Instance.gridSize.Item1;
        [SerializeField] private int gridSizeY = Globals.Instance.gridSize.Item2;

        private bool isEmptySave;

        public static void Save()
        {
            SavesManager.Save(new GameSave(), Path);
        }

        public static void Load()
        {
            var save = SavesManager.Load<GameSave>(Path);

            if (save == null) CreateEmptySave().Apply();
            else save.Apply();
        }

        public override void Apply()
        {
            NodeController.Instance.currentNodeIndex = currentVertex;
            NodeController.Instance.noNodeIsChosen = noNodeIsChosen;

            JsonUtility.FromJsonOverwrite(playerData, Player.Data);

            Globals.Instance.seed = seed;
            Globals.Instance.gridSize = (gridSizeX, gridSizeY);

            Battle.SceneManager.enemyGroup = ScriptableObject.CreateInstance<EnemyGroup>();
            JsonUtility.FromJsonOverwrite(enemyGroup, Battle.SceneManager.enemyGroup);

            ShopManager.goods = Tools.Json.JsonToListScriptableObjects<Good>(goods);

            TreasureManager.treasure =
                ScriptableObject.CreateInstance<LongScythe>(); // TODO doesn't work
            // No matter what type of LootItem instance is created json overwriting works

            JsonUtility.FromJsonOverwrite(treasure, TreasureManager.treasure);

            NodeController.Instance.RegenerateNodes();

            if (isEmptySave)
                foreach (Item item in Player.Data.items)
                    item.OnGet();


            SceneManager.LoadScene(scene);
        }

        public static GameSave CreateEmptySave()
        {
            return new GameSave
            {
                currentVertex = -1,
                noNodeIsChosen = true,
                scene = "Map",
                playerData = JsonUtility.ToJson(
                    Object.Instantiate(Resources.Load<PlayerData>("NewGamePreset"))),
                seed = Globals.Instance.randomSeed
                    ? Random.Range(0, (int)Math.Pow(10, 6))
                    : Globals.Instance.seed,
                enemyGroup = NullObject,
                treasure = JsonUtility.ToJson(null),
                goods = EmptyList,
                isEmptySave = true
            };
        }
    }
}