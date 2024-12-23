using Battle.Modifiers;
using Other;
using UnityEngine;

namespace Battle.Grid.Cells.MovingCells.MatchingCells
{
    public class Ice : Match3Cell, IModifierAble
    {
        [SerializeField] private int freezeAmount;

        public ModifierList Modifiers { get; } = new();
        public int Value => freezeAmount;

        public override string Description =>
            descriptionKeyRef.Value.IndexErrorProtectedFormat(freezeAmount);
        public override bool IsSameType(Cell other) => other is Ice;

        protected override void Use()
        {
            BattleFlowManager.Instance.CurrentlyTurningUnit.damage.mods.Add(
                new Units.StatModifiers.Frozen(
                    IIntModifier.UseModList(Modifiers.List, freezeAmount)));
        }
    }
}