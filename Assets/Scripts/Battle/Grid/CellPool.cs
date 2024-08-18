using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle.Grid
{
    /// A buffer object that stores and gives certain Cells.
    /// This object HAS to be used in order to create and delete Cells.
    /// It is needed to prevent lags caused by often created and destroyed Cells.
    public class CellPool : MonoBehaviour
    {
        public int maxSize;

        private readonly List<Cell> _cells = new();
        public static CellPool Instance { get; private set; }

        public void Awake()
        {
            Instance = this;
        }

        /// <summary>
        ///     Get a new instance of cell by given example of cell.
        ///     For example, if you pass an instance of Attack cell, it will return a new instance of Attack cell.
        /// </summary>
        /// <param name="example">A cell with the same type will be returned.</param>
        /// <returns>A new instance of given cell.</returns>
        public Cell Acquire(Cell example)
        {
            var cell = _cells.FirstOrDefault(cell => cell.IsSameType(example));

            if (cell != null) _cells.Remove(cell);
            else cell = CreateMissingCell(example);

            return cell;
        }

        /// Hides and disables cell.
        /// This function is supposed to be used instead of simple
        /// <see cref="Object.Destroy(UnityEngine.Object)"/>
        public void Release(Cell cell)
        {
            cell.transform.SetParent(transform, false);
            cell.gameObject.SetActive(false);

            if (_cells.Count >= maxSize)
                Destroy(cell.gameObject);
            else
                _cells.Add(cell);
        }

        private Cell CreateMissingCell(Cell example)
        {
            var cell = Instantiate(example, transform);
            cell.gameObject.SetActive(false);

            return cell;
        }
    }
}