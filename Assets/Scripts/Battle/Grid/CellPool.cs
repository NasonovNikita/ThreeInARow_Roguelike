using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle.Grid
{
    public class CellPool : MonoBehaviour
    {
        public int maxSize;

        private readonly List<Cell> cells = new();
        public static CellPool Instance { get; private set; }

        public void Awake()
        {
            Instance = this;
        }

        public Cell Acquire(Cell example)
        {
            Cell cell = cells.FirstOrDefault(cell => cell.IsSameType(example));

            if (cell != null) cells.Remove(cell);
            else cell = CreateMissingCell(example);

            return cell;
        }

        public void Release(Cell cell)
        {
            cell.transform.SetParent(transform, false);
            cell.gameObject.SetActive(false);

            if (cells.Count >= maxSize)
                Destroy(cell.gameObject);
            else
                cells.Add(cell);
        }

        private Cell CreateMissingCell(Cell example)
        {
            Cell cell = Instantiate(example, transform);
            cell.gameObject.SetActive(false);

            return cell;
        }
    }
}