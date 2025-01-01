using System.Collections.Generic;
using Battle.Grid;
using Battle.Grid.Cells.MovingCells;
using Battle.Units;
using Other;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "UseAnyCell", menuName = "Spells/UseAnyCell")]
    public class UseAnyCell : Spell
    {
        private bool _chosen = true;

        public override void Init(Unit unit)
        {
            base.Init(unit);
            
            Grid.Grid.Instance.OnCellClicked += UseCell;
        }

        protected override void Action()
        {
            _chosen = false;
        }

        private void UseCell(Cell cell)
        {
            if (_chosen) return;

            if (cell is not MatchingCell matchingCell) return;
            
            BattleFlowManager.Instance.AddProcess(new SmartCoroutine(BattleFlowManager.Instance,
                matchingCell.Process, onEnd: () =>
                {
                    GridGenerator.Instance.ReplaceCellsByCoordinates(new List<(int, int)>
                        { Grid.Grid.Instance.FindCell(cell) });
                }).Start());
            
            _chosen = true;
        }
    }
}