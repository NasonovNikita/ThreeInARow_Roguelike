using Battle.Modifiers;
using Other;
using UnityEngine;

namespace Battle.Grid.Cells.MovingCells.MatchingCells
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
            var unit = BattleFlowManager.Instance.CurrentlyTurningUnit;

            unit.target.TakeDamage(
                unit.damage.ApplyDamage(IIntModifier.UseModList(Modifiers.List,
                    baseAttackVal)));
            unit.InvokeOnMadeHit();
        }

        public override bool IsSameType(Cell other) => other is Attack;
    }
}