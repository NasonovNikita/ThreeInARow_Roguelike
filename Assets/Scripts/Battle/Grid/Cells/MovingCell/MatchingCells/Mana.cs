using Battle.Grid.Modifiers;
using Battle.Modifiers;
using UnityEngine;

namespace Battle.Grid.Cells.MovingCell.MatchingCells
{
    public class Mana : MatchingCell, IModifierAble
    {
        [SerializeField] private int amount;

        public ModifierList Modifiers { get; } = new();

        public int Value => amount;

        protected override void Use()
        {
            BattleFlowManager.Instance.CurrentlyTurningUnit.mana.Refill(
                IIntModifier.UseModList(Modifiers.ModList, amount));
        }

        public override bool IsSameType(Cell other)
        {
            return other is Mana;
        }

        public override string Description =>
            Other.Tools.IndexErrorProtectedFormat(descriptionKeyRef.Value, amount);
    }
}