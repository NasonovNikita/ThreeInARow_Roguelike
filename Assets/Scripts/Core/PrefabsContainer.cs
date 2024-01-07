using Map.Vertexes;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class PrefabsContainer : MonoBehaviour
    {
        public GameObject winMessage;

        public GameObject loseMessage;

        public BattleVertex battleVertex;

        public ShopVertex shopVertex;

        public TreasureVertex treasureVertex;

        public Image pickerAim;

        public static PrefabsContainer instance;
        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        
            DontDestroyOnLoad(gameObject);
        }
    }
}
