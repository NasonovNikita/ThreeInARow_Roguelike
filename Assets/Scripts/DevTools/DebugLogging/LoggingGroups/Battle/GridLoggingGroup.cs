#if UNITY_EDITOR

using Battle.Grid;

namespace DevTools.DebugLogging.LoggingGroups.Battle
{
    public class GridLoggingGroup : DebugLoggingGroup
    {
        public override void Attach()
        {
            Grid.Instance.OnSwitchedCells += (cell1, cell2) =>
                CheckAndWrite($"Switched cells {cell1} and {cell2}");
        }

        public override void UnAttach()
        {
            // ignored
        }
    }
}

#endif