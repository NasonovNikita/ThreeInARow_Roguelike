using System.Linq;
using Battle.Units;
using Other;
using UnityEngine;

namespace Battle.Match3
{
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] private Grid grid;

        public void Awake()
        {
            Generate();
        }

        private void Generate()
        {
            var box = new Cell[grid.sizeY, grid.sizeX];
            do
            {
                for (int i = 0; i < grid.sizeY; i++)
                {
                    for (int j = 0; j < grid.sizeY; j++)
                    {
                        box[i, j] = Tools.Random.RandomChoose(Player.data.cells);
                    }
                }
                grid.SetBox(box);
            } while (!Player.data.cells.Aggregate(true, (prev, cell) => prev && cell.BoxIsStable));
        }

        public void Refill()
        {
            var box = grid.Box;
            
            
        }
    }
}