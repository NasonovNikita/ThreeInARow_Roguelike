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
        #region windows
        
        public GameObject winMessage;

        public GameObject loseMessage;

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

        public UnitHUD unitHUD;

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
