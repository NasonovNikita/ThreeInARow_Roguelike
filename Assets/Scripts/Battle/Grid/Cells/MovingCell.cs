using System.Collections;
using Battle.Units;
using Other;
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
                !BattleFlowManager.Instance.AllowedToUseGrid) return;

            
            BattleFlowManager.Instance.AddProcess(new SmartCoroutine(this, Choose)
                .Start());
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

            yield return scaleFirst;
            yield return scaleSecond;
        }
    }
}