using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle.Units;
using Other;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battle.Match3.MatchingCells
{
    public abstract class MatchingCell : Cell, IPointerClickHandler
    {
        private static MatchingCell _chosen;

        [SerializeField] private ObjectMover mover;
        [SerializeField] private ObjectScaler scaler;

        private (bool, bool) finishedOperation;
        private bool Finished => finishedOperation is { Item1: true, Item2: true };
        protected Unit TurningUnit => BattleFlowManager.Instance.CurrentlyTurningUnit;

        private Cell[,] box;

        public static event Action<MatchingCell> OnCellUsed;
        
        public override void Awake()
        {
            base.Awake();
            box = grid.box;
        }

        public IEnumerator Choose()
        {
            if (_chosen == null)
            {
                _chosen = this;
                StartCoroutine(scaler.ScaleUp());
            }
            else if (_chosen == this)
            {
                StartCoroutine(_chosen.scaler.UnScale());
                _chosen = null;
            }
            else if (grid.CellsAreNeighbours(_chosen, this))
            {
                yield return StartCoroutine(MoveCells());

                yield return StartCoroutine(ScaleCells());
                
                OnMoveDone();
            }
            else
            {
                StartCoroutine(_chosen.scaler.UnScale());
                StartCoroutine(scaler.ScaleUp());
                _chosen = this;
            }
        }

        private IEnumerator MoveCells()
        {
            finishedOperation = (false, false);
            
            yield return StartCoroutine(scaler.ScaleUp());
            StartCoroutine(mover.MoveTo(_chosen.transform.position,
                () => finishedOperation.Item2 = true));
            StartCoroutine(_chosen.mover.MoveTo(transform.position, 
                () => finishedOperation.Item1 = true));

            yield return new WaitUntil(() => Finished);
        }

        private IEnumerator ScaleCells()
        {
            finishedOperation = (false, false);
                
            StartCoroutine(scaler.UnScale(() => finishedOperation.Item1 = true));
            StartCoroutine(_chosen.scaler.UnScale(() => finishedOperation.Item2 = true));
                
            yield return new WaitUntil(() => Finished);
        }

        private void OnMoveDone()
        {
            grid.SwitchCells(this, _chosen);
            _chosen = null;
            
            var rowedCells = FindRowedCells();
            
            UseGems(rowedCells);
            DeleteGems(rowedCells);
            
            TurningUnit?.WasteMove();
            grid.StartCoroutine(generator.Refill());
        }

        private List<MatchingCell> FindRowedCells()
        {
            var found = new HashSet<MatchingCell>();

            for (int i = 0; i < grid.sizeY; i++)
            {
                for (int j = 0; j < grid.sizeX; j++)
                {
                    if (i < grid.sizeY - 2 && RowExists(i, j, di: 1))
                    {
                        found.UnionWith(new[]
                        {
                            (MatchingCell)box[i, j],
                            (MatchingCell)box[i + 1, j],
                            (MatchingCell)box[i + 2, j]
                        });
                    }
                    if (j < grid.sizeX - 2 && RowExists(i, j, dj: 1))
                    {
                        found.UnionWith(new[]
                        {
                            (MatchingCell)box[i, j],
                            (MatchingCell)box[i, j + 1],
                            (MatchingCell)box[i, j + 2]
                        });
                    }
                }
            }

            return found.ToList();
        }

        private static void UseGems(List<MatchingCell> cells)
        {
            // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
            foreach (MatchingCell cell in cells)
            {
                if (BattleFlowManager.Instance.CurrentlyTurningUnit != BattleFlowManager.NoTurningUnit)
                    cell.LowUse();
            }
        }

        private void LowUse()
        {
            OnCellUsed?.Invoke(this);
            
            Use();
        }

        protected abstract void Use();

        private static void DeleteGems(List<MatchingCell> cells)
        {
            foreach (MatchingCell cell in cells)
            {
                cell.Delete();
            }
        }

        private bool RowExists(int i, int j, int di = 0, int dj = 0)
        {
            return   box[i, j] is MatchingCell cell1 &&
                     box[i + di, j + dj] is MatchingCell cell2 &&
                     box[i + di * 2, j + dj * 2] is MatchingCell cell3 &&
                     cell1.IsSameType(cell2) && 
                     cell2.IsSameType(cell3);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left || TurningUnit is not Player) return;
            
            StartCoroutine(Choose());
        }

        public override bool BoxIsStable(Cell[,] boxToCheck)
        {
            grid = FindFirstObjectByType<Grid>();
            box = boxToCheck;

            return FindRowedCells().Count == 0;
        }
    }
}