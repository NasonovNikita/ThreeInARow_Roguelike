using System.Collections.Generic;
using Battle.Grid;
using Battle.Grid.Cells;
using Battle.Grid.Cells.MovingCells;
using Battle.Units;
using Other;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "UseAnyCell", menuName = "Spells/UseAnyCell")]
    public class UseAnyCell : Spell
    {
        public override int UseCost => _currentChosenRarity is null ? 0 : _costs[(Rarity)_currentChosenRarity];

        private readonly Dictionary<Rarity, int> _costs = new()
        {
            { Rarity.Common, 10 },
            { Rarity.Rare, 15 },
            { Rarity.Epic, 25 },
            { Rarity.Legendary, 40 },
            { Rarity.Secret, 40 }
        };

        private Rarity? _currentChosenRarity;

        public override void Init(Unit unit)
        {
            base.Init(unit);

            Grid.Grid.Instance.OnCellClicked += TrackRarity;
        }

        protected override void Action()
        {
           UseCell(MovingCell.Chosen);
        }

        private void TrackRarity(Cell cell)
        {
            if (MovingCell.Chosen != cell)
            {
                _currentChosenRarity = null;
                InvokeOnChanged();
                return;
            }
            
            _currentChosenRarity = cell?.rarity;
            InvokeOnChanged();
        }

        private void UseCell(Cell cell)
        {
            if (cell is not MatchingCell matchingCell) return;

            var coordinates = Grid.Grid.Instance.FindCell(cell);
            BattleFlowManager.Instance.AddProcess(new SmartCoroutine(BattleFlowManager.Instance,
                matchingCell.Process, onEnd: () =>
                {
                    if (cell.IsInGridBox) return;
                    
                    new SmartCoroutine(BattleFlowManager.Instance, matchingCell.UnChoose).Start();
                    GridGenerator.Instance.ReplaceCellsByCoordinates(new List<(int, int)>
                        { coordinates });
                }).Start());
        }
    }
}