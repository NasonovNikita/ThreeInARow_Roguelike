using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle.Units;
using Other;
using UnityEngine;

namespace Battle.Grid.Cells.MovingCells
{
    public class MatchingCellsManager : MonoBehaviour
    {
        private List<(int, int)> _cellsCoordinates = new();

        private SmartCoroutine _onMoveDoneProcess;
        private List<MatchingCell> _rowedCells;

        public void Start()
        {
            if (_onMoveDoneProcess != null) return;

            _onMoveDoneProcess = new SmartCoroutine(this, ProcessAllCells,
                GetCellsToUse,
                RefillGrid);

            Grid.Instance.OnChanged += Process;
            return;

            void GetCellsToUse()
            {
                _rowedCells = new List<MatchingCell>();
                foreach (var cell in Player.Data.cells.Where(cell => cell is MatchingCell))
                {
                    _rowedCells.AddRange(((MatchingCell)cell).GetCellsToUse());
                }
                _cellsCoordinates = GetCellsCoordinates();
            }
        }

        private void Process() => BattleFlowManager.Instance.Processes.Add(_onMoveDoneProcess.TryRestart());


        private IEnumerator ProcessAllCells()
        {
            // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
            foreach (var cell in _rowedCells) yield return cell.Process();
        }

        private void RefillGrid()
        {
            GridGenerator.Instance.ReplaceCellsByCoordinates(
                _cellsCoordinates);
        }

        private List<(int, int)> GetCellsCoordinates() =>
            _rowedCells.Select(cell => Grid.Instance.FindCell(cell)).ToList();
    }
}