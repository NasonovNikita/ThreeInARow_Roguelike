using Battle.UI.HUD;
using Battle.UI.ModsDisplaying;
using Map.Nodes;
using UI;
using UI.MessageWindows;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Singleton
{
    public class PrefabsContainer : MonoBehaviour
    {
        public static PrefabsContainer Instance;

        public InventoryItem inventoryItem;

        public GameObject inventoryManager;

        public ModIcon modIcon;

        public HUD hud;

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #region windows

        public GameObject winMessage;

        public SpellGettingWarningWindow spellGettingWarningWindow;

        public SpellReplacingWindow spellReplacingWindow;

        public LanguageChooser languageChooser;

        #endregion

        #region vertexes

        [FormerlySerializedAs("battleVertex")] public BattleNode battleNode;

        [FormerlySerializedAs("shopVertex")] public ShopNode shopNode;

        [FormerlySerializedAs("treasureVertex")]
        public TreasureNode treasureNode;

        #endregion
    }
}