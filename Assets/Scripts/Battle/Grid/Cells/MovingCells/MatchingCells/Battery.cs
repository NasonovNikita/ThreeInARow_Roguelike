using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle.Modifiers;
using Other;
using UnityEngine;


namespace Battle.Grid.Cells.MovingCells.MatchingCells
{
    public class Battery : Match3Cell, IModifierAble
    {
        [SerializeField] private int damage;
        [SerializeField] private int addition;
        [SerializeField] private int timesToIncrease;

        [SerializeField] private int count;
        
        public override string Description => descriptionKeyRef.Value.FormatByKeys(
            new Dictionary<string, object>
            {
                {"damage", damage},
                {"multiplier", addition},
                {"timesToIncrease", timesToIncrease}
            });

        public override bool IsSameType(Cell other) => other is Battery;

        public override bool BoxIsStable(Cell[,] box)
        {
            return base.BoxIsStable(box) && Tools.MultiDimToOne(box).Count(IsSameType) <= Grid.Instance.Size / 6;
        }

        protected override void Use()
        {
            var unit = BattleFlowManager.Instance.CurrentlyTurningUnit;

            unit.target.TakeDamage(
                unit.damage.ApplyDamage(
                    IIntModifier.UseModList(Modifiers.List, damage)));
            
            unit.InvokeOnMadeHit();

            if (timesToIncrease <= 0) return;

            damage += addition;
            timesToIncrease--;
        }

        protected override IEnumerator OnUsed()
        {
            var second = GridGenerator.Instance.CellToShuffleWith(this);
            
            yield return new SmartCoroutine(this, () => SwitchCells(this, second))
                .Start();
            Grid.Instance.SwitchCells(this, second);
        }

        public ModifierList Modifiers { get; } = new();
        public int Value => damage;
    }
}