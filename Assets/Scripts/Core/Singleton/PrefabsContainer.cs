using Map.Nodes;
using UI;
using UI.Battle.HUD;
using UI.Battle.ModsDisplaying;
using UI.MessageWindows;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Singleton
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
        
        [FormerlySerializedAs("battleVertex")] public BattleNode battleNode;

        [FormerlySerializedAs("shopVertex")] public ShopNode shopNode;

        [FormerlySerializedAs("treasureVertex")] public TreasureNode treasureNode;
        
        #endregion

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
