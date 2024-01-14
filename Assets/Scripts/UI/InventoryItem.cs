using Core;
using Other;
using UI.MessageWindows;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InventoryItem : MonoBehaviour
    {
        private GetAble item;
        public static void Create(GetAble targetItem, Transform parentTransform)
        {
            InventoryItem res = Instantiate(PrefabsContainer.instance.inventoryItem, parentTransform);
            res.item = targetItem;
        }

        public void Start()
        {
            GetComponentInChildren<Text>().text = item.Title;
            if (item.img != null) GetComponentInChildren<Image>().sprite = item.img;
            GetComponent<DevDebugAbleObject>().text = item.Description;
        }
    }
}