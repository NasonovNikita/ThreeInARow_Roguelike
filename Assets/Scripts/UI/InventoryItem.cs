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
        private LootItem _item;

        public void Start()
        {
            titleText.text = _item.Title;
            sprite.sprite = _item.img != null ? _item.img : defaultSprite;
            infoObjectText.text = _item.Description;
        }

        public static void Create(LootItem targetItem, Transform parentTransform)
        {
            var res =
                Instantiate(PrefabsContainer.Instance.inventoryItem, parentTransform);
            res._item = targetItem;
        }
    }
}