using Battle.Grid.Modifiers;
using Battle.Modifiers;
using Battle.Units;

namespace Battle.Grid.Cells.MovingCell.MatchingCells
{
    public class Hourglass : MatchingCell, IModifierAble
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
            
            Player.Instance.AddMoves(IIntModifier.UseModList(Modifiers.ModList, Value));
            _added = true;
        }

        public ModifierList Modifiers { get; } = new();

        public int Value => 1;
    }
}