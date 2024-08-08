using System.Collections.Generic;
using Battle.Units;
using Other;
using UnityEngine;

namespace UI
{
    public class InventoryGrid : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryWindow;
        private readonly List<LootItem> getAble = new();

        public void Awake()
        {
            getAble.AddRange(Player.Data.items);
            getAble.AddRange(Player.Data.spells);

            foreach (LootItem item in getAble) InventoryItem.Create(item, transform);
        }

        public void Close()
        {
            Destroy(inventoryWindow.gameObject);
        }
    }
}