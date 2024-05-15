using System.Collections.Generic;
using Battle.Units;
using Other;
using UnityEngine;

namespace UI
{
    public class InventoryGrid : MonoBehaviour
    {
        private readonly List<LootItem> getAble = new();
        
        [SerializeField] private GameObject inventoryWindow;
        public void Awake()
        {
            getAble.AddRange(Player.data.items);
            getAble.AddRange(Player.data.spells);

            foreach (var item in getAble)
            {
                InventoryItem.Create(item, transform);
            }
        }

        public void Close()
        {
            Destroy(inventoryWindow.gameObject);
        }
    }
}