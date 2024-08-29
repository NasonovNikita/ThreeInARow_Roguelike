using Battle.Modifiers;
using Other;
using UnityEngine;

namespace Battle.Grid.Cells.MovingCells.MatchingCells
{
    public class HeavyAttack : RowingCell, IModifierAble
    {
        protected override int CountInRow => 4;
        
        [SerializeField] private int baseAttackVal;

        public override string Description =>
            descriptionKeyRef.Value.IndexErrorProtectedFormat(baseAttackVal);

        public ModifierList Modifiers { get; } = new();

        public int Value => baseAttackVal;

        protected override void Use()
        {
            var unit = BattleFlowManager.Instance.CurrentlyTurningUnit;

            unit.target.TakeDamage(
                (int)(unit.damage.ApplyDamage(IIntModifier.UseModList(Modifiers.List,
                    baseAttackVal)) * 1.5f));
            unit.InvokeOnMadeHit();
        }

        public override bool IsSameType(Cell other) => other is HeavyAttack;
    }
}