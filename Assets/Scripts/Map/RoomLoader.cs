using Other;
using Shop;
using Treasure;
using Generator = Map.Nodes.Managers.Generator;
using Random = UnityEngine.Random;

namespace Map
{
    public static class RoomLoader
    {
        public static void LoadBattle(int layer, int seed, bool isBoss)
        {
            Random.InitState(seed);

            Battle.SceneManager.enemyGroup =
                isBoss
                    ? Generator.Instance.ChooseBoss(layer)
                    : Generator.Instance.ChooseBattleEnemyGroup(layer);

            UnityEngine.SceneManagement.SceneManager.LoadScene("Battle");

            ResetRandom();
        }

        public static void LoadShop(int layer, int seed)
        {
            Random.InitState(seed);

            ShopManager.goods = Generator.Instance.ChooseGoods(layer);
            ShopManager.entered = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Shop");

            ResetRandom();
        }

        public static void LoadTreasure(int layer, int seed)
        {
            TreasureManager.treasure = Generator.Instance.ChooseTreasure(layer);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Treasure");
        }

        private static void ResetRandom()
        {
            Tools.Random.ResetRandom();
        }
    }
}