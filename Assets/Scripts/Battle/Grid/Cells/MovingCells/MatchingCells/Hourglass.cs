using Battle.Modifiers;
using Battle.Units;

namespace Battle.Grid.Cells.MovingCells.MatchingCells
{
    public class Hourglass : Match3Cell, IModifierAble
    {
        private static bool _added;

        public override string Description => descriptionKeyRef.Value;

        public void Awake()
        {
            BattleFlowManager.Instance.OnBattleEnd += () => _added = false;
            BattleFlowManager.Instance.OnCycleEnd += () => _added = false;
        }

        public ModifierList Modifiers { get; } = new();

        public int Value => 1;

        public override bool IsSameType(Cell other) => other is Hourglass;

        protected override void Use()
        {
            if (_added) return;

            Player.Instance.AddMoves(IIntModifier.UseModList(Modifiers.List, Value));
            _added = true;
        }
    }
}