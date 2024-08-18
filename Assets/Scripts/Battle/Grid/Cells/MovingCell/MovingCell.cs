using System.Collections;
using Battle.Units;
using Other;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battle.Grid.Cells.MovingCell
{
    /// <summary>
    ///     A cell that can be moved in grid.
    ///     Touch two neighboring MovingCells to swap them.
    /// </summary>
    public abstract class MovingCell : Cell, IPointerClickHandler
    {
        private static MovingCell _chosen;

        [SerializeField] private ObjectMover mover;
        [SerializeField] private ObjectScaler scaler;

        private (bool, bool) _finishedOperation;
        private bool Finished => _finishedOperation == (true, true);

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left ||
                BattleFlowManager.Instance.CurrentlyTurningUnit is not Player) return;

            StartCoroutine(Choose());
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
            else if (Grid.Instance.CellsAreNeighbours(_chosen, this))
            {
                yield return StartCoroutine(MoveCells());

                yield return StartCoroutine(ScaleCells());

                Grid.Instance.SwitchCells(this, _chosen);
                OnMoveDone();
                _chosen.OnMoveDone();

                _chosen = null;

                (BattleFlowManager.Instance.CurrentlyTurningUnit as Player)?.WasteMove();
            }
            else
            {
                StartCoroutine(_chosen.scaler.UnScale());
                StartCoroutine(scaler.ScaleUp());
                _chosen = this;
            }
        }

        protected virtual void OnMoveDone()
        {
        }

        private IEnumerator MoveCells()
        {
            _finishedOperation = (false, false);

            yield return StartCoroutine(scaler.ScaleUp());
            StartCoroutine(mover.MoveTo(_chosen.transform.position,
                () => _finishedOperation.Item2 = true));
            StartCoroutine(_chosen.mover.MoveTo(transform.position,
                () => _finishedOperation.Item1 = true));

            yield return new WaitUntil(() => Finished);
        }

        private IEnumerator ScaleCells()
        {
            _finishedOperation = (false, false);

            StartCoroutine(scaler.UnScale(() => _finishedOperation.Item1 = true));
            StartCoroutine(_chosen.scaler.UnScale(() => _finishedOperation.Item2 = true));

            yield return new WaitUntil(() => Finished);
        }
    }
}