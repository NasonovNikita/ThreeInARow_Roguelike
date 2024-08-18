using System.Collections.Generic;
using Battle.Units;
using Other;
using UnityEngine;

namespace UI
{
    public class InventoryGrid : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryWindow;
        private readonly List<LootItem> _getAble = new();

        public void Awake()
        {
            _getAble.AddRange(Player.Data.items);
            _getAble.AddRange(Player.Data.spells);

            foreach (var item in _getAble) InventoryItem.Create(item, transform);
        }

        public void Close()
        {
            Destroy(inventoryWindow.gameObject);
        }
    }
}