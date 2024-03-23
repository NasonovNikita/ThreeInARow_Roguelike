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

        private (bool, bool) finishedSwitch;
        private bool Switched => finishedSwitch.Item1 && finishedSwitch.Item2;

        protected Unit TurningUnit => manager.CurrentlyTurningUnit;
        private BattleManager manager;

        protected abstract void Use();
        
        public override void Awake()
        {
            base.Awake();
            manager = FindFirstObjectByType<BattleManager>();
        }

        public IEnumerator Choose()
        {
            if (_chosen == null)
            {
                _chosen = this;
                StartCoroutine(scaler.Scale());
            }
            else if (grid.CellsAreNeighbours(_chosen, this))
            {
                yield return StartCoroutine(scaler.Scale()); // Make Coroutine
                StartCoroutine(mover.MoveTo(_chosen.transform.position,
                    () => { finishedSwitch.Item2 = true; }));
                StartCoroutine(_chosen.mover.MoveTo(transform.position,
                    () => { finishedSwitch.Item1 = true; }));

                yield return new WaitUntil(() => Switched);
                
                OnMoveDone();
            }
            else
            {
                StartCoroutine(_chosen.scaler.UnScale());
                StartCoroutine(scaler.Scale());
                _chosen = this;
            }
        }

        private void OnMoveDone()
        {
            var rowedCells = FindRowedCells();
            
            UseGems(rowedCells);
            DeleteGems(rowedCells);
            
            generator.Refill();
            TurningUnit.WasteMove();
        }

        private List<MatchingCell> FindRowedCells()
        {
            var box = grid.Box;
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
            foreach (var cell in cells)
            {
                cell.Use();
            }
        }

        private static void DeleteGems(List<MatchingCell> cells)
        {
            foreach (var cell in cells)
            {
                Destroy(cell.gameObject);
            }
        }

        private bool RowExists(int i, int j, int di = 0, int dj = 0)
        {
            var box = grid.Box;
            return   box[i, j] is MatchingCell &&
                     box[i + di, j + dj] is MatchingCell &&
                     box[i + di * 2, j + dj * 2] is MatchingCell && 
                     ((MatchingCell)box[i, j]).IsSameType((MatchingCell)box[i + di, j + dj]) && 
                     ((MatchingCell)box[i + di, j + dj]).IsSameType((MatchingCell)box[i + di * 2, j + dj * 2]);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left || TurningUnit is not Player) return;
            
            StartCoroutine(Choose());
        }

        public override bool BoxIsStable
        {
            get
            {
                grid = FindFirstObjectByType<Grid>();
                return FindRowedCells().Count == 0;
            }
        }

        protected abstract bool IsSameType(MatchingCell second);
    }
}