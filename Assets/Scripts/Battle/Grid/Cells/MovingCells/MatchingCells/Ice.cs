using Battle.Modifiers;
using Battle.Units.StatModifiers;
using Other;
using UnityEngine;

namespace Battle.Grid.Cells.MovingCells.MatchingCells
{
    public class Ice : Match3Cell, IModifierAble
    {
        [SerializeField] private int value;

        public ModifierList Modifiers { get; } = new();
        public int Value => value;

        public override string Description =>
            descriptionKeyRef.Value.IndexErrorProtectedFormat(value);
        public override bool IsSameType(Cell other) => other is Ice;

        protected override void Use()
        {
            BattleFlowManager.Instance.CurrentlyTurningUnit.target.Statuses.Add(
                new Frozen(value));
        }
    }
}