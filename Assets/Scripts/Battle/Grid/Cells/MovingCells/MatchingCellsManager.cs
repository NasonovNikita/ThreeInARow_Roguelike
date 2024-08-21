using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Other;
using UnityEngine;

namespace Battle.Grid.Cells.MovingCells
{
    public class MatchingCellsManager : MonoBehaviour
    {
        private List<(int, int)> _cellsToDeleteCoordinates = new();

        private SmartCoroutine _onMoveDoneProcess;
        private List<MatchingCell> _rowedCells;
        public static MatchingCellsManager Instance { get; private set; }

        public void Awake()
        {
            Instance = this;
        }

        public void Start()
        {
            if (_onMoveDoneProcess != null) return;

            _onMoveDoneProcess = new SmartCoroutine(this, ProcessAllCells,
                SetRowedCellsData,
                RefillGrid);
            BattleFlowManager.Instance.Processes.Add(_onMoveDoneProcess);
            return;

            void SetRowedCellsData()
            {
                _rowedCells = FindRowedCells();
                _cellsToDeleteCoordinates = GetRowedCellsCoordinates();
            }
        }

        public void Process() => _onMoveDoneProcess.TryRestart();


        private IEnumerator ProcessAllCells()
        {
            foreach (var cell in _rowedCells) yield return cell.Process();
        }

        private void RefillGrid()
        {
            GridGenerator.Instance.ReplaceCellsByCoordinates(
                _cellsToDeleteCoordinates);
        }

        private List<(int, int)> GetRowedCellsCoordinates() =>
            _rowedCells.Select(cell => Grid.Instance.FindCell(cell)).ToList();

        public List<MatchingCell> FindRowedCells()
        {
            var found = new HashSet<MatchingCell>();

            for (var i = 0; i < Grid.Instance.sizeY; i++)
            for (var j = 0; j < Grid.Instance.sizeX; j++)
            {
                if (i < Grid.Instance.sizeY - 2 && RowExists(i, j, 1))
                    found.UnionWith(new[]
                    {
                        (MatchingCell)Grid.Instance.Box[i, j],
                        (MatchingCell)Grid.Instance.Box[i + 1, j],
                        (MatchingCell)Grid.Instance.Box[i + 2, j]
                    });
                if (j < Grid.Instance.sizeX - 2 && RowExists(i, j, dj: 1))
                    found.UnionWith(new[]
                    {
                        (MatchingCell)Grid.Instance.Box[i, j],
                        (MatchingCell)Grid.Instance.Box[i, j + 1],
                        (MatchingCell)Grid.Instance.Box[i, j + 2]
                    });
            }

            return found.ToList();
        }

        private bool RowExists(int i, int j, int di = 0, int dj = 0) =>
            Grid.Instance.Box[i, j] is MatchingCell cell1 &&
            Grid.Instance.Box[i + di, j + dj] is MatchingCell cell2 &&
            Grid.Instance.Box[i + di * 2, j + dj * 2] is MatchingCell cell3 &&
            cell1.IsSameType(cell2) &&
            cell2.IsSameType(cell3);
    }
}