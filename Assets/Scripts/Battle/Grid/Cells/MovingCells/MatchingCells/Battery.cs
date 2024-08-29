using System.Collections;
using System.Collections.Generic;
using Battle.Modifiers;
using Other;
using Unity.VisualScripting;
using UnityEngine;

namespace Battle.Grid.Cells.MovingCells.MatchingCells
{
    public class Battery : Match3Cell, IModifierAble
    {
        [SerializeField] private int damage;
        [SerializeField] private float multiplier;
        [SerializeField] private int timesToIncrease;

        public override string Description => descriptionKeyRef.Value.FormatByKeys(
            new Dictionary<string, object>
            {
                {"damage", damage},
                {"multiplier", multiplier},
                {"timesToIncrease", timesToIncrease}
            });

        public override bool IsSameType(Cell other) => other is Battery;

        protected override void Use()
        {
            var unit = BattleFlowManager.Instance.CurrentlyTurningUnit;

            unit.target.TakeDamage(
                unit.damage.ApplyDamage(
                    IIntModifier.UseModList(Modifiers.List, damage)));
            
            unit.InvokeOnMadeHit();

            if (timesToIncrease <= 0) return;
            
            damage = (int)(damage * multiplier);
            timesToIncrease--;
        }

        protected override IEnumerator OnUsed()
        {
            int x;
            int y;
            do
            {
                x = Random.Range(0, Grid.Instance.sizeX);
                y = Random.Range(0, Grid.Instance.sizeY);
            } while (Grid.Instance.Box[x, y] is Battery);

            var second = Grid.Instance.Box[x, y];
            yield return new SmartCoroutine(this, () => SwitchCells(this, second))
                .Start();
            Grid.Instance.SwitchCells(this, Grid.Instance.Box[x, y]);
        }

        public ModifierList Modifiers { get; } = new();
        public int Value => damage;
    }
}