namespace Battle.Grid.Cells.MovingCells.MatchingCells
{
    /// Created for DRY (Don't Repeat Yourself)
    public abstract class Match3Cell : RowingCell
    {
        protected override int CountInRow => 3;
    }
}