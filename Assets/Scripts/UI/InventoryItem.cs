using Core.Singleton;
using Other;
using UI.MessageWindows;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InventoryItem : MonoBehaviour
    {
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Text titleText;
        [SerializeField] private Image sprite;
        [SerializeField] private InfoObject infoObjectText;
        private LootItem item;
        
        public static void Create(LootItem targetItem, Transform parentTransform)
        {
            InventoryItem res = Instantiate(PrefabsContainer.instance.inventoryItem, parentTransform);
            res.item = targetItem;
        }

        public void Start()
        {
            titleText.text = item.Title;
            sprite.sprite = item.img != null ? item.img : defaultSprite;
            infoObjectText.text = item.Description;
        }
    }
}