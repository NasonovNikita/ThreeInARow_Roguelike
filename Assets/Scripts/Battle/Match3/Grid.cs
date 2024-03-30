using System;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Battle.Match3
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private RectTransform cellPrefab;
        private RectTransform[,] points;
        
        public int sizeX;
        public int sizeY;
        public Cell[,] box;

        private ObjectPool<Cell> cells;

        public void Awake()
        {
            points = new RectTransform[sizeY,sizeX];

            for (int i = 0; i < sizeY; i++)
            for (int j = 0; j < sizeX; j++)
                points[i, j] = Instantiate(cellPrefab, transform);
        }

        public void CreateEmptyBox() => box = new Cell[sizeY, sizeX];

        public void SwitchCells(Cell first, Cell second)
        {
            var firstIndex = FindCell(first);
            var secondIndex = FindCell(second);

            (box[firstIndex.Item1, firstIndex.Item2], box[secondIndex.Item1, secondIndex.Item2]) = 
                (box[secondIndex.Item1, secondIndex.Item2], box[firstIndex.Item1, firstIndex.Item2]);
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
            
            var cellTransform = cell.transform;
            
            cellTransform.SetParent(points[i, j].transform, false);
            cellTransform.localPosition = Vector3.zero;
        }

        public bool CellsAreNeighbours(Cell cell1, Cell  cell2)
        {
            var pos1 = FindCell(cell1);
            var pos2 = FindCell(cell2);
        
            return pos1.Item1 == pos2.Item1 && Math.Abs(pos1.Item2 - pos2.Item2) == 1 ||
                   pos1.Item2 == pos2.Item2 && Math.Abs(pos1.Item1 - pos2.Item1) == 1;
        }
    }
}