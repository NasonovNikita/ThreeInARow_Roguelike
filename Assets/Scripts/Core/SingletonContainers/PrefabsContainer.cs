using Map.Vertexes;
using UI;
using UI.Battle.HUD;
using UI.Battle.ModsDisplaying;
using UI.MessageWindows;
using UnityEngine;
using UnityEngine.UI;

namespace Core.SingletonContainers
{
    public class PrefabsContainer : MonoBehaviour
    {
        #region windows
        
        public GameObject winMessage;

        public SpellGettingWarningWindow spellGettingWarningWindow;

        public SpellReplacingWindow spellReplacingWindow;

        public LanguageChooser languageChooser;
        
        #endregion

        #region vertexes
        
        public BattleVertex battleVertex;

        public ShopVertex shopVertex;

        public TreasureVertex treasureVertex;
        
        #endregion

        public Image pickerAim;

        public InventoryItem inventoryItem;

        public GameObject inventoryManager;

        public ModIcon modIcon;

        public HUD hud;

        public static PrefabsContainer instance;
        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(gameObject);
        }
    }
}
