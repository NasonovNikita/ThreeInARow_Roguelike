using System.Collections.Generic;
using System.Linq;

namespace Battle.Grid.Cells.MovingCells
{
    public abstract class RowingCell : MatchingCell
    {
        public override List<MatchingCell> GetCellsToUse()
        {
            var found = new HashSet<RowingCell>();

            for (var i = 0; i < Grid.Instance.sizeY; i++)
            for (var j = 0; j < Grid.Instance.sizeX; j++)
            {
                if (i < Grid.Instance.sizeY - 2 && RowExists(i, j, di: 1))
                {
                    for (var di = 0; di < CountInRow; di++)
                    {
                        found.Add((RowingCell)Grid.Instance.Box[i + di, j]);
                    }
                }

                // ReSharper disable once InvertIf
                if (j < Grid.Instance.sizeX - 2 && RowExists(i, j, dj: 1))
                {
                    for (var dj = 0; dj < CountInRow; dj++)
                    {
                        found.Add((RowingCell)Grid.Instance.Box[i, j + dj]);
                    }
                }
            }

            return found.Cast<MatchingCell>().ToList();
        }

        private bool RowExists(int i, int j, int di = 0, int dj = 0)
        {
            var cells = new List<Cell>();
            for (var k = 0; k < CountInRow; k++)
            {
                cells.Add(Grid.Instance.Box[i + di * k, j + dj * k]);
            }

            return cells.All(IsSameType);
        }
    }
}