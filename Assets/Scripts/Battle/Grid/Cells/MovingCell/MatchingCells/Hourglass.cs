using Battle.Units;

namespace Battle.Grid.Cells.MovingCell.MatchingCells
{
    public class Hourglass : MatchingCell
    {
        private static bool _added;

        public void Awake()
        {
            BattleFlowManager.Instance.OnBattleEnd += () => _added = false;
            BattleFlowManager.Instance.OnCycleEnd += () => _added = false;
        }

        public override bool IsSameType(Cell other)
        {
            return other is Hourglass;
        }

        public override string Description => descriptionKeyRef.Value;

        protected override void Use()
        {
            if (_added) return;
            
            Player.Instance.AddMove();
            _added = true;
        }
    }
}