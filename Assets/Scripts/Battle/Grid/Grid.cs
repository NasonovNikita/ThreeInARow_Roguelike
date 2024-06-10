using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Battle.Grid
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private RectTransform cellPrefab;

        public int sizeX;
        public int sizeY;
        public Cell[,] box;

        private ObjectPool<Cell> cells;
        private RectTransform[,] points;

        public static Grid Instance { get; private set; }

        public void Awake()
        {
            Instance = this;

            points = new RectTransform[sizeY, sizeX];

            for (var i = 0; i < sizeY; i++)
            for (var j = 0; j < sizeX; j++)
                points[i, j] = Instantiate(cellPrefab, transform);
        }

        public event Action<Cell, Cell> OnSwitchedCells;

        public event Action OnChanged;

        public void CreateEmptyBox()
        {
            box = new Cell[sizeY, sizeX];
        }

        public void SwitchCells(Cell first, Cell second)
        {
            var (x1, y1) = FindCell(first);
            var (x2, y2) = FindCell(second);

            (box[x1, y1], box[x2, y2]) =
                (box[x2, y2], box[x1, y1]);

            OnSwitchedCells?.Invoke(box[x1, y1], box[x2, y2]);
            OnChanged?.Invoke();
        }

        public void SetCell(Cell newCell, int i, int j)
        {
            RemoveCell(box[i, j]);

            box[i, j] = newCell;
            newCell.isInGridBox = true;
        }

        public void InitGrid()
        {
            for (var i = 0; i < sizeY; i++)
            for (var j = 0; j < sizeX; j++)
                InitCell(box[i, j], i, j);

            OnChanged?.Invoke();
        }

        public (int, int) FindCell(Object cell)
        {
            for (var i = 0; i < sizeY; i++)
            for (var j = 0; j < sizeX; j++)
                if (box[i, j] == cell)
                    return (i, j);

            throw new InvalidOperationException();
        }

        public List<Cell> GetCellNeighbours(Cell cell)
        {
            (int, int) pos = FindCell(cell);

            var neighbours = new List<Cell>();

            for (var di = -1; di <= 1; di++)
            for (var dj = -1; dj <= 1; dj++)
                try
                {
                    neighbours.Add(box[pos.Item1 + di, pos.Item2 + dj]);
                }
                catch
                {
                    // ignored
                }

            return neighbours;
        }

        private void InitCell(Cell cell, int i, int j)
        {
            cell.gameObject.SetActive(true);

            Transform cellTransform = cell.transform;

            cellTransform.SetParent(points[i, j].transform, false);
            cellTransform.localPosition = Vector3.zero;
        }

        private void RemoveCell(Cell cell)
        {
            if (cell == null || !cell.isInGridBox) return;
            
            cell.isInGridBox = false;
            CellPool.Instance.Release(cell);
        }

        public bool CellsAreNeighbours(Cell cell1, Cell cell2)
        {
            (int, int) pos1 = FindCell(cell1);
            (int, int) pos2 = FindCell(cell2);

            return (pos1.Item1 == pos2.Item1 && Math.Abs(pos1.Item2 - pos2.Item2) == 1) ||
                   (pos1.Item2 == pos2.Item2 && Math.Abs(pos1.Item1 - pos2.Item1) == 1);
        }
    }
}