using Battle.Modifiers;
using Other;
using UnityEngine;

namespace Battle.Grid.Cells.MovingCells.MatchingCells
{
    public class ToolSet : Match3Cell, IModifierAble
    {
        [SerializeField] private int count;

        public override string Description =>
            descriptionKeyRef.Value.IndexErrorProtectedFormat(count);

        public override bool IsSameType(Cell other) => other is ToolSet;

        protected override void Use()
        {
            throw new System.NotImplementedException();
        }

        public ModifierList Modifiers { get; } = new();
        public int Value => count;
    }
}