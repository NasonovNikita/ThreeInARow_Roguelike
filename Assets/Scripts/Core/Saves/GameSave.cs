using System;
using Battle.Items;
using Battle.Units;
using Map.Nodes.Managers;
using Other;
using Shop;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Core.Saves
{
    /// <summary>
    ///     Contains data about last game run
    ///     (map position, player, and last rooms data to be able to load instantly).
    /// </summary>
    [Serializable]
    public class GameSave : SaveObject
    {
        private const string Path = "/Game.dat";

        private const string NullObject = "{}";
        private const string EmptyList = "{[]}";

        [SerializeField]
        private int currentVertex = NodeController.Instance.CurrentNodeIndex;

        [SerializeField]
        private bool noNodeIsChosen = NodeController.Instance.NoNodeIsChosen;

        [SerializeField] private string scene =
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        [SerializeField] private string playerData = JsonUtility.ToJson(Player.Data);
        [SerializeField] private int seed = Globals.Instance.seed;

        [SerializeField]
        private string enemyGroup = JsonUtility.ToJson(Battle.SceneManager.EnemyGroup);

        [SerializeField]
        private string goods = Tools.Json.ListToJson(Shop.SceneManager.Goods);

        [SerializeField]
        private string treasure = JsonUtility.ToJson(Treasure.SceneManager.Treasure);

        [SerializeField] private int gridSizeX = Globals.Instance.GridSize.Item1;
        [SerializeField] private int gridSizeY = Globals.Instance.GridSize.Item2;

        private bool _isEmptySave;

        public static void Save()
        {
            SavesManager.Save(new GameSave(), Path);
        }

        /// <summary>
        ///     Loads data from memory.
        /// </summary>
        public static GameSave Load() =>
            SavesManager.Load<GameSave>(Path) ?? CreateEmptySave();

        public override void Apply()
        {
            NodeController.Instance.CurrentNodeIndex = currentVertex;
            NodeController.Instance.NoNodeIsChosen = noNodeIsChosen;

            JsonUtility.FromJsonOverwrite(playerData, Player.Data);

            Globals.Instance.seed = seed;
            Globals.Instance.GridSize = (gridSizeX, gridSizeY);

            Battle.SceneManager.EnemyGroup =
                ScriptableObject.CreateInstance<EnemyGroup>();
            JsonUtility.FromJsonOverwrite(enemyGroup, Battle.SceneManager.EnemyGroup);

            Shop.SceneManager.Goods = Tools.Json.JsonToListScriptableObjects<Good>(goods);

            Treasure.SceneManager.Treasure =
                ScriptableObject.CreateInstance<LongScythe>(); // TODO doesn't work?
            // No matter what type of LootItem instance is created json overwriting works

            JsonUtility.FromJsonOverwrite(treasure, Treasure.SceneManager.Treasure);

            NodeController.Instance.RegenerateNodes();

            if (_isEmptySave)
                foreach (var item in Player.Data.items)
                    item.OnGet();


            UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
        }

        public static GameSave CreateEmptySave() =>
            new()
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
                treasure = NullObject,
                goods = EmptyList,
                _isEmptySave = true
            };
    }
}