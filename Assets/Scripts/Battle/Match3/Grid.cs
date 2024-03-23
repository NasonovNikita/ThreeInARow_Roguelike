using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Battle.Match3
{
    public class Grid : MonoBehaviour
    {
        public int sizeX;
        public int sizeY;

        [SerializeField] private RectTransform cellPrefab;
        
        private RectTransform[,] points;
        public Cell[,] Box { get; private set; } = { };

        public void Awake()
        {
            points = new RectTransform[sizeY,sizeX];

            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    points[i, j] = Instantiate(cellPrefab, transform);
                }
            }
        }

        public void Start()
        {
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    Box[i, j] = InstantiateCell(Box[i, j], i, j);
                }
            }
        }

        public void SetBox(Cell[,] newBox)
        {
            foreach (var cell in Box)
            {
                if (cell != null) Destroy(cell.gameObject);
            }

            Box = newBox;
        }
    
        private (int, int) FindGem(Object cell)
        {
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    if (Box[i, j] == cell)
                    {
                        return (i, j);
                    }
                }
            }

            throw new InvalidOperationException();
        }

        private Cell InstantiateCell(Cell cell, int i, int j)
        {
            cell = Instantiate(cell, points[i, j]);
            
            return cell;
        }

        public bool CellsAreNeighbours(Cell cell1, Cell  cell2)
        {
            var pos1 = FindGem(cell1);
            var pos2 = FindGem(cell2);
        
            return pos1.Item1 == pos2.Item1 && Math.Abs(pos1.Item2 - pos2.Item2) == 1 ||
                   pos1.Item2 == pos2.Item2 && Math.Abs(pos1.Item1 - pos2.Item1) == 1;
        }
    }
}