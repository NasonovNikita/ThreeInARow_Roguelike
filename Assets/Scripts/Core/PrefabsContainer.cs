using Map.Vertexes;
using UI;
using UI.Battle;
using UI.MessageWindows;
using UnityEngine;
using UnityEngine.Serialization;
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

        public InventoryItem inventoryItem;

        public GameObject inventoryManager;

        public SpellGettingWarningWindow spellGettingWarningWindow;

        public SpellReplacingWindow spellReplacingWindow;

        public LanguageChooser languageChooser;

        public ModIcon modIcon;

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
