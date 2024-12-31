using Other;
using Generator = Map.Nodes.Managers.Generator;
using Random = UnityEngine.Random;

namespace Map
{
    /// <summary>
    ///     Loads specific rooms with set random seed.
    ///     Uses <see cref="Generator"/> to load some data at the time of arrival.
    /// </summary>
    public static class RoomLoader
    {
        public static void LoadBattle(int layer, int seed, bool isBoss)
        {
            Random.InitState(seed);

            Battle.SceneManager.EnemyGroup =
                isBoss
                    ? Generator.Instance.ChooseBoss(layer)
                    : Generator.Instance.ChooseBattleEnemyGroup(layer);

            UnityEngine.SceneManagement.SceneManager.LoadScene("Battle");

            ResetRandom();
        }

        public static void LoadShop(int layer, int seed)
        {
            Random.InitState(seed);

            Shop.SceneManager.Goods = Generator.Instance.ChooseGoods(layer);
            Shop.SceneManager.Entered = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Shop");

            ResetRandom();
        }

        public static void LoadTreasure(int layer, int seed)
        {
            Random.InitState(seed);

            Treasure.SceneManager.Treasure = Generator.Instance.ChooseTreasure(layer);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Treasure");

            ResetRandom();
        }

        private static void ResetRandom()
        {
            Tools.Random.ResetRandom();
        }
    }
}