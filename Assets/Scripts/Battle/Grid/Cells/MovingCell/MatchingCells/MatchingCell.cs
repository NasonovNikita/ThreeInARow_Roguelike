using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle.Units;
using Core.Singleton;
using UnityEngine;

namespace Battle.Grid.Cells.MovingCell.MatchingCells
{
    public abstract class MatchingCell : MovingCell
    {
        private static int _cellsToUseCount;
        private static bool _refilled;
        private static List<(int, int)> _cellsToDeleteCoordinates = new();
        
        protected override void OnMoveDone()
        {
            _refilled = false;
            
            var rowedCells = FindRowedCells();

            foreach (MatchingCell cell in rowedCells)
            {
                _cellsToDeleteCoordinates.Add(Grid.Instance.FindCell(cell));
            }
            
            _cellsToUseCount += 1;

            BattleFlowManager.Instance.endedProcesses.Add(() => _cellsToUseCount == 0);
            BattleFlowManager.Instance.endedProcesses.Add(() => _refilled);
            Grid.Instance.StartCoroutine(UseAndHideCells(rowedCells));
        }

        private IEnumerator UseAndHideCells(List<MatchingCell> cells)
        {
            foreach (MatchingCell cell in cells)
            {
                if (BattleFlowManager.Instance.CurrentlyTurningUnit is Player) cell.Use();
                OffScreenPoint.Instance.Hide(cell.gameObject);

                yield return new WaitForSeconds(0.3f);

            }
            _cellsToUseCount--;

            // ReSharper disable once InvertIf
            if (_cellsToUseCount == 0 && !_refilled)
            {
                GridGenerator.Instance.ReplaceCellsByCoordinates(_cellsToDeleteCoordinates);
                
                _cellsToDeleteCoordinates = new List<(int, int)>();
                _refilled = true;
            }
        }

        private List<MatchingCell> FindRowedCells()
        {
            var found = new HashSet<MatchingCell>();

            for (var i = 0; i < Grid.Instance.sizeY; i++)
            for (var j = 0; j < Grid.Instance.sizeX; j++)
            {
                if (i < Grid.Instance.sizeY - 2 && RowExists(i, j, 1))
                    found.UnionWith(new[]
                    {
                        (MatchingCell)Grid.Instance.box[i, j],
                        (MatchingCell)Grid.Instance.box[i + 1, j],
                        (MatchingCell)Grid.Instance.box[i + 2, j]
                    });
                if (j < Grid.Instance.sizeX - 2 && RowExists(i, j, dj: 1))
                    found.UnionWith(new[]
                    {
                        (MatchingCell)Grid.Instance.box[i, j],
                        (MatchingCell)Grid.Instance.box[i, j + 1],
                        (MatchingCell)Grid.Instance.box[i, j + 2]
                    });
            }

            return found.ToList();
        }

        protected abstract void Use();

        private bool RowExists(int i, int j, int di = 0, int dj = 0)
        {
            return Grid.Instance.box[i, j] is MatchingCell cell1 &&
                   Grid.Instance.box[i + di, j + dj] is MatchingCell cell2 &&
                   Grid.Instance.box[i + di * 2, j + dj * 2] is MatchingCell cell3 &&
                   IsSameType(cell1) &&
                   IsSameType(cell2) &&
                   IsSameType(cell3);
        }

        public override bool BoxIsStable(Cell[,] box)
        {
            return FindRowedCells().Count == 0;
        }
    }
}