using System;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Battle.Match3
{
    public class Grid : MonoBehaviour
    {
        public static Grid Instance { get; private set; }
        
        [SerializeField] private RectTransform cellPrefab;
        private RectTransform[,] points;
        
        public int sizeX;
        public int sizeY;
        public Cell[,] box;

        private ObjectPool<Cell> cells;

        public event Action<Cell, Cell> OnSwitchedCells;

        public void Awake()
        {
            Instance = this;
            
            points = new RectTransform[sizeY,sizeX];

            for (int i = 0; i < sizeY; i++)
            for (int j = 0; j < sizeX; j++)
                points[i, j] = Instantiate(cellPrefab, transform);
        }

        public void CreateEmptyBox() => box = new Cell[sizeY, sizeX];

        public void SwitchCells(Cell first, Cell second)
        {
            (int x1, int y1) = FindCell(first);
            (int x2, int y2) = FindCell(second);

            (box[x1, y1], box[x2, y2]) = 
                (box[x2, y2], box[x1, y1]);
            
            OnSwitchedCells?.Invoke(box[x1, y1], box[x2, y2]);
        }

        public void SetCell(Cell newCell, int i, int j)
        {
            if (box[i, j] != null && box[i, j].isActiveAndEnabled) box[i, j].Delete();

            box[i, j] = newCell;
        }

        private (int, int) FindCell(Object cell)
        {
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    if (box[i, j] == cell) return (i, j);
                }
            }

            throw new InvalidOperationException();
        }

        public void InitGrid()
        {
            for (int i = 0; i < sizeY; i++)
            for (int j = 0; j < sizeX; j++) 
                InitCell(box[i, j], i, j);
        }
        
        private void InitCell(Cell cell, int i, int j)
        {
            cell.gameObject.SetActive(true);
            
            Transform cellTransform = cell.transform;
            
            cellTransform.SetParent(points[i, j].transform, false);
            cellTransform.localPosition = Vector3.zero;
        }

        public bool CellsAreNeighbours(Cell cell1, Cell  cell2)
        {
            (int, int) pos1 = FindCell(cell1);
            (int, int) pos2 = FindCell(cell2);
        
            return pos1.Item1 == pos2.Item1 && Math.Abs(pos1.Item2 - pos2.Item2) == 1 ||
                   pos1.Item2 == pos2.Item2 && Math.Abs(pos1.Item1 - pos2.Item1) == 1;
        }
    }
}