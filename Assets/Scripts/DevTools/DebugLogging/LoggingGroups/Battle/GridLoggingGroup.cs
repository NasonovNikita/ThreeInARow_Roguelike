using Battle.Match3;
using Battle.Match3.MatchingCells;

namespace DevTools.DebugLogging.LoggingGroups.Battle
{
    public class GridLoggingGroup : DebugLoggingGroup
    {
        public override void Attach()
        {
            Grid.Instance.OnSwitchedCells += (cell1, cell2) => CheckAndWrite($"Switched cells {cell1} and {cell2}");
            MatchingCell.OnCellUsed += WriteCellUsed;
        }

        public override void UnAttach()
        {
            MatchingCell.OnCellUsed -= WriteCellUsed;
        }

        private void WriteCellUsed(Cell cell) => 
            CheckAndWrite($"Used cell {cell}");
    }
}