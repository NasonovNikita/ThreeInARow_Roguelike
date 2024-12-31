using Battle.Modifiers;
using Other;
using UnityEngine;

namespace Battle.Grid.Cells.MovingCells.MatchingCells
{
    public class Shield : Match3Cell, IModifierAble
    {
        [SerializeField] private int amount;

        public override string Description =>
            descriptionKeyRef.Value.IndexErrorProtectedFormat(amount);

        public ModifierList Modifiers { get; } = new();

        public int Value => amount;

        protected override void Use()
        {
            BattleFlowManager.Instance.CurrentlyTurningUnit.hp.onTakingDamageMods.Add(
                new Units.StatModifiers.Shield(
                    IIntModifier.UseModList(Modifiers.List, amount)));
        }

        public override bool IsSameType(Cell other) => other is Shield;
    }
}