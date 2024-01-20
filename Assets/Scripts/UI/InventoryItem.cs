using Core;
using Other;
using UI.MessageWindows;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InventoryItem : MonoBehaviour
    {
        private LootItem item;
        public static void Create(LootItem targetItem, Transform parentTransform)
        {
            InventoryItem res = Instantiate(PrefabsContainer.instance.inventoryItem, parentTransform);
            res.item = targetItem;
        }

        public void Start()
        {
            GetComponentInChildren<Text>().text = item.Title;
            GetComponentInChildren<Image>().sprite = item.img != null ? item.img : SpritesContainer.instance.empty;
            GetComponent<DevDebugAbleObject>().text = item.Description;
        }
    }
}