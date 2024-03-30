using System.Collections;
using System.Linq;
using Battle.Units;
using Other;
using UnityEngine;

namespace Battle.Match3
{
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] private Grid grid;
        [SerializeField] private float refillTime;

        private static Cell RandomCell =>
            CellPool.Instance.Acquire(Tools.Random.RandomChoose(Player.data.cells));
        
        private static bool BoxIsStable(Cell[,] box) =>
            Player.data.cells.Aggregate(true, (prev, cell) => prev && cell.BoxIsStable(box));

        public void Awake() => Generate();

        private void Generate()
        {
            grid.CreateEmptyBox();

            do
            {
                for (int i = 0; i < grid.sizeY; i++)
                for (int j = 0; j < grid.sizeX; j++) 
                    grid.SetCell(RandomCell, i, j);
            } while (!BoxIsStable(grid.box));
            
            
            
            grid.InitGrid();
        }

        public IEnumerator Refill()
        {
            do
            {
                for (int i = 0; i < grid.sizeY; i++)
                for (int j = 0; j < grid.sizeX; j++) 
                    if (!grid.box[i, j].isActiveAndEnabled) grid.SetCell(RandomCell, i, j);
            } while (!BoxIsStable(grid.box));

            yield return new WaitForSeconds(refillTime);
            
            grid.InitGrid();
        }
    }
}