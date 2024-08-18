using Battle.Modifiers;
using Battle.Units;
using Other;
using UnityEngine;

namespace Battle.Grid.Cells.MovingCell.MatchingCells
{
    public class Healing : MatchingCell, IModifierAble
    {
        [SerializeField] private int amount;

        public override string Description =>
            descriptionKeyRef.Value.IndexErrorProtectedFormat(amount);

        public int Value => amount;

        public ModifierList Modifiers { get; } = new();

        public override bool IsSameType(Cell other)
        {
            return other is Healing;
        }

        protected override void Use()
        {
            Player.Instance.hp.Heal(IIntModifier.UseModList(Modifiers.List, amount));
        }
    }
}