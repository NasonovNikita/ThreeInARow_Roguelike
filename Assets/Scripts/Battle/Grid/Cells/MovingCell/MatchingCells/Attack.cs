using Battle.Grid.Modifiers;
using Battle.Modifiers;
using Battle.Units;
using Other;
using UnityEngine;

namespace Battle.Grid.Cells.MovingCell.MatchingCells
{
    public class Attack : MatchingCell, IModifierAble
    {
        [SerializeField] private int baseAttackVal;

        public override string Description =>
            descriptionKeyRef.Value.IndexErrorProtectedFormat(baseAttackVal);

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
    }
}