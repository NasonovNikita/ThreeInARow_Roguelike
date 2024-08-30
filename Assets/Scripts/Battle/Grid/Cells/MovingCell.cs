using System.Collections;
using Battle.Grid.Cells.MovingCells;
using Battle.Units;
using Other;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battle.Grid.Cells
{
    /// <summary>
    ///     A cell that can be moved in grid.
    ///     Choose two neighboring MovingCells to swap them.
    /// </summary>
    public abstract class MovingCell : Cell, IPointerClickHandler
    {
        private static MovingCell _chosen;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left ||
                BattleFlowManager.Instance.CurrentlyTurningUnit is not Player) return;

            StartCoroutine(Choose());
        }

        public IEnumerator Choose()
        {
            if (_chosen == null) // This is first one
            {
                _chosen = this;
                yield return StartCoroutine(scaler.ScaleUp());
            }
            else if (_chosen == this) // Tried to choose the same = unchoose
            {
                var coroutine = new SmartCoroutine(this,
                    () => scaler.Unscale());
                _chosen = null;
                coroutine.Start();
            }
            else if (Grid.Instance.CellsAreNeighbours(_chosen, this)) // And switch
            {
                var scaleSecond = new SmartCoroutine(this,
                    scaler.ScaleUp);
                var switchCells = new SmartCoroutine(this,
                    () => SwitchCells(_chosen, this));
                var unscaleCells = new SmartCoroutine(this,
                    UnscaleCells);

                BattleFlowManager.Instance.Processes.Add(scaleSecond);
                BattleFlowManager.Instance.Processes.Add(switchCells);
                BattleFlowManager.Instance.Processes.Add(unscaleCells);

                (BattleFlowManager.Instance.CurrentlyTurningUnit as Player)?.WasteMove();

                
                yield return scaleSecond.Start();
                yield return switchCells.Start();
                yield return unscaleCells.Start();
            
                
                Grid.Instance.SwitchCells(this, _chosen);

                OnMoveDone();
                _chosen.OnMoveDone();

                _chosen = null;
            }
            else // Choose another one
            {
                var unscaleFirst = new SmartCoroutine(this,
                    () => _chosen.scaler.Unscale()).Start();
                var unscaleSecond = new SmartCoroutine(this,
                    () => scaler.ScaleUp()).Start();
                _chosen = this;

                BattleFlowManager.Instance.Processes.Add(unscaleFirst);
                BattleFlowManager.Instance.Processes.Add(unscaleSecond);

                yield return unscaleFirst;
                yield return unscaleSecond;
            }
        }

        // Maybe redundant
        protected virtual void OnMoveDone()
        {
        }

        private IEnumerator UnscaleCells()
        {
            var scaleFirst = new SmartCoroutine(this,
                () => scaler.Unscale()).Start();
            var scaleSecond = new SmartCoroutine(this,
                () => _chosen.scaler.Unscale()).Start();

            yield return new WaitUntil(() => scaleFirst.Finished && scaleSecond.Finished);
        }
    }
}