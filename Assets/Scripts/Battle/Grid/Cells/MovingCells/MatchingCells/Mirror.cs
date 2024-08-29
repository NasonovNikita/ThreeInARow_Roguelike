using Battle.Modifiers;
using Battle.Units.StatModifiers;
using Other;
using UnityEngine;

namespace Battle.Grid.Cells.MovingCells.MatchingCells
{
    public class Mirror : Match3Cell, IModifierAble
    {
        [SerializeField] private int value;

        public override string Description =>
            descriptionKeyRef.Value.IndexErrorProtectedFormat(value);

        public override bool IsSameType(Cell other) => other is Mirror;

        protected override void Use()
        {
            BattleFlowManager.Instance.CurrentlyTurningUnit.hp.onTakingDamageMods.Add(
                new Reflection(value));
        }

        public ModifierList Modifiers { get; } = new();
        public int Value => value;
    }
}