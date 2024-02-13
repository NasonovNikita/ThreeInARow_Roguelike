using System;
using Battle;
using Battle.Items;
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
    public class GameSave : Save
    {
        [SerializeField] private int currentVertex;
        [SerializeField] private string scene;
        [SerializeField] private string playerData;
        [SerializeField] private int seed;
        [SerializeField] private string enemyGroup;
        [SerializeField] private string goods;
        private bool preset;

        public GameSave()
        {
            currentVertex = Map.Map.currentVertex;
            scene = SceneManager.GetActiveScene().name;
            playerData = JsonUtility.ToJson(Player.data);
            seed = Globals.instance.seed;
            enemyGroup = JsonUtility.ToJson(BattleManager.enemyGroup);
            goods = Tools.Json.ListToJson(ShopManager.goods);
        }

        public override void Load()
        {
            Map.Map.currentVertex = currentVertex;
            
            JsonUtility.FromJsonOverwrite(playerData, Player.data);
            if (preset) foreach (Item item in Player.data.items) item.OnGet();
            
            Globals.instance.seed = seed;

            if (BattleManager.enemyGroup == null)
                BattleManager.enemyGroup = ScriptableObject.CreateInstance<EnemyGroup>();
            JsonUtility.FromJsonOverwrite(enemyGroup, BattleManager.enemyGroup);
            
            ShopManager.goods = Tools.Json.JsonToList<Good>(goods);
            Map.Map.Regenerate();
            
            SceneManager.LoadScene(scene);
        }

        public GameSave CreateEmptySave()
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