using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Battle.Grid
{
    /// Grid contains Cells in Battle and provides some container-like features such as FindCell, SetCell,
    /// and also grid-specific such as SwitchCells, GetCellsNeighbours, CellsAreNeighbours.
    public class Grid : MonoBehaviour
    {
        [SerializeField] private RectTransform cellPrefab;

        public int sizeX;
        public int sizeY;

        private ObjectPool<Cell> _cellPool;
        private RectTransform[,] _points;

        /// A [,] array with cells in Grid as-they-are.
        /// Invariant: cells are stored the same as they are in Grid visually in game.
        /// <remarks>Don't change directly!</remarks>
        public Cell[,] Box;

        public static Grid Instance { get; private set; }

        /// Is called when two cells were switched using
        /// <see cref="SwitchCells"/>
        /// function.
        public event Action<Cell, Cell> OnSwitchedCells;

        public event Action OnChanged;

        public void Awake()
        {
            Instance = this;

            _points = new RectTransform[sizeY, sizeX];

            for (var i = 0; i < sizeY; i++)
            for (var j = 0; j < sizeX; j++)
                _points[i, j] = Instantiate(cellPrefab, transform);
        }

        /// Replaces Grid's Box with a new empty one
        public void CreateEmptyBox()
        {
            Box = new Cell[sizeY, sizeX];
        }

        /// <summary>
        ///     Switches two cells in Grid's <see cref="Box"/> locally.
        /// </summary>
        /// <remarks>Cells are not physically/visually moved.</remarks>
        public void SwitchCells(Cell first, Cell second)
        {
            var (x1, y1) = FindCell(first);
            var (x2, y2) = FindCell(second);
            SwitchCellsMuted(first, second);

            OnSwitchedCells?.Invoke(Box[x1, y1], Box[x2, y2]);
            OnChanged?.Invoke();
        }

        public void SwitchCellsMuted(Cell first, Cell second)
        {
            var (x1, y1) = FindCell(first);
            var (x2, y2) = FindCell(second);

            (Box[x1, y1], Box[x2, y2]) =
                (Box[x2, y2], Box[x1, y1]);
        }

        public void InvokeSwitchCells()
        {
            
        }

        /// <summary>
        /// Removes old cell and sets a new one in
        /// <see cref="Box"/>[i, j].
        /// </summary>
        public void SetCell(Cell newCell, int i, int j)
        {
            RemoveCell(Box[i, j]);

            Box[i, j] = newCell;
            newCell.IsInGridBox = true;
        }

        /// <summary>
        /// The same as <see cref="SetCell"/> but also invokes <see cref="OnChanged"/>.
        /// </summary>
        public void SetCellInvoked(Cell newCell, int i, int j)
        {
            SetCell(newCell, i, j);
            
            OnChanged?.Invoke();
        }

        public void InitGrid()
        {
            for (var i = 0; i < sizeY; i++)
            for (var j = 0; j < sizeX; j++)
                InitCell(Box[i, j], i, j);
        }

        public (int, int) FindCell(Object cell)
        {
            for (var i = 0; i < sizeY; i++)
            for (var j = 0; j < sizeX; j++)
                if (Box[i, j] == cell)
                    return (i, j);

            throw new InvalidOperationException();
        }

        /// <summary>
        ///     Returns all Cell's neighbours. Neighbour is a cell that has a common side or common diagonal with a given one.
        ///     For instance, if cell is not on an endge of a Grid it will have 8 neighbours in total.
        /// </summary>
        /// <param name="cell">Which cell's neighbours are to be found.</param>
        /// <returns>List of neighbours</returns>
        public List<Cell> GetCellNeighbours(Cell cell)
        {
            var pos = FindCell(cell);

            var neighbours = new List<Cell>();

            for (var di = -1; di <= 1; di++)
            for (var dj = -1; dj <= 1; dj++)
                try
                {
                    neighbours.Add(Box[pos.Item1 + di, pos.Item2 + dj]);
                }
                catch
                {
                    // ignored
                }

            return neighbours;
        }

        /// Places a Cell in Grid and activates it
        private void InitCell(Cell cell, int i, int j)
        {
            cell.gameObject.SetActive(true);

            var cellTransform = cell.transform;

            cellTransform.SetParent(_points[i, j].transform, false);
            cellTransform.localPosition = Vector3.zero;
        }

        private void RemoveCell(Cell cell)
        {
            if (cell == null || !cell.IsInGridBox) return;

            cell.IsInGridBox = false;
            CellPool.Instance.Release(cell);
        }

        /// Cells are considered neighbours if they have a common side or common diagonal in Grid.
        /// <returns>True if cells are neighbours in Grid.</returns>
        public bool CellsAreNeighbours(Cell cell1, Cell cell2)
        {
            var pos1 = FindCell(cell1);
            var pos2 = FindCell(cell2);

            return (pos1.Item1 == pos2.Item1 && Math.Abs(pos1.Item2 - pos2.Item2) == 1) ||
                   (pos1.Item2 == pos2.Item2 && Math.Abs(pos1.Item1 - pos2.Item1) == 1);
        }
    }
}