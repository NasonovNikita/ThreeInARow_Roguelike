using Battle.Grid.Modifiers;
using Battle.Modifiers;
using Battle.Units;
using UnityEngine;

namespace Battle.Grid.Cells.MovingCell.MatchingCells
{
    public class Attack : MatchingCell, IModifierAble
    {
        [SerializeField] private int baseAttackVal;

        public ModifierList Modifiers { get; } = new();

        public int Value => baseAttackVal;

        protected override void Use()
        {
            Unit unit = BattleFlowManager.Instance.CurrentlyTurningUnit;

            unit.target.TakeDamage(
                unit.damage.ApplyDamage(IIntModifier.UseModList(Modifiers.ModList, baseAttackVal)));
            unit.InvokeOnMadeHit();
        }

        public override bool IsSameType(Cell other)
        {
            return other is Attack;
        }

        public override string Description =>
            Other.Tools.IndexErrorProtectedFormat(descriptionKeyRef.Value, baseAttackVal);
    }
}