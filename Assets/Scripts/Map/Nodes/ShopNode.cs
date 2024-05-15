using Core.Singleton;
using Map.Nodes.Managers;
using Shop;

namespace Map.Nodes
{
    public class ShopNode : Node
    {
        protected override void Action()
        {
            SetNodeRandom();
            
            ShopManager.goods = Generator.Instance.ChooseGoods(layer);
            ShopManager.entered = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Shop");
            
            ResetRandom();
        }

        public static ShopNode Create(int layer, int randomSeed)
        {
            return (ShopNode)Node.Create(PrefabsContainer.instance.shopNode, layer, randomSeed);
        }
    }
}