using Battle.Modifiers;
using Other;
using UnityEngine;

namespace Battle.Grid.Cells.MovingCells.MatchingCells
{
    public class Mana : MatchingCell, IModifierAble
    {
        [SerializeField] private int amount;

        public override string Description =>
            descriptionKeyRef.Value.IndexErrorProtectedFormat(amount);

        public ModifierList Modifiers { get; } = new();

        public int Value => amount;

        protected override void Use()
        {
            BattleFlowManager.Instance.CurrentlyTurningUnit.mana.Refill(
                IIntModifier.UseModList(Modifiers.List, amount));
        }

        public override bool IsSameType(Cell other) => other is Mana;
    }
}