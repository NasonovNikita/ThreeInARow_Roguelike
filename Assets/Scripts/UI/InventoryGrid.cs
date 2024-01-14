using System.Collections.Generic;
using Battle.Units;
using Other;
using UnityEngine;

namespace UI
{
    public class InventoryGrid : MonoBehaviour
    {
        private readonly List<GetAble> getAbles = new();
        [SerializeField] private GameObject inventoryWindow;
        public void Awake()
        {
            getAbles.AddRange(Player.data.items);
            getAbles.AddRange(Player.data.spells);

            foreach (GetAble getAble in getAbles)
            {
                InventoryItem.Create(getAble, transform);
            }
        }

        public void Close()
        {
            Destroy(inventoryWindow.gameObject);
        }
    }
}