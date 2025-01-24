using System;
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
        public static MovingCell Chosen { get; private set; }

        public event Action OnClicked;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left ||
                !BattleFlowManager.Instance.AllowedToUseGrid) return;
            
            
            BattleFlowManager.Instance.AddProcess(new SmartCoroutine(this, Choose)
                .Start());
            OnClicked?.Invoke();
        }

        public IEnumerator UnChoose()
        {
            yield return StartCoroutine(Choose());
        }

        public IEnumerator Choose()
        {
            if (Chosen == null) // This is first one
            {
                Chosen = this;
                yield return StartCoroutine(scaler.ScaleUp());
            }
            else if (Chosen == this) // Tried to choose the same = unchoose
            {
                var coroutine = new SmartCoroutine(this,
                    () => scaler.Unscale());
                Chosen = null;
                coroutine.Start();
            }
            else if (Grid.Instance.CellsAreNeighbours(Chosen, this)) // And switch
            {
                var scaleSecond = new SmartCoroutine(this,
                    scaler.ScaleUp);
                var switchCells = new SmartCoroutine(this,
                    () => SwitchCells(Chosen, this));
                var unscaleCells = new SmartCoroutine(this,
                    UnscaleCells);

                (BattleFlowManager.Instance.CurrentlyTurningUnit as Player)?.WasteMove();

                
                yield return scaleSecond.Start();
                yield return switchCells.Start();
                yield return unscaleCells.Start();
            
                
                Grid.Instance.SwitchCells(this, Chosen);

                OnMoveDone();
                Chosen.OnMoveDone();

                Chosen = null;
            }
            else // Choose another one
            {
                var unscaleFirst = new SmartCoroutine(this,
                    () => Chosen.scaler.Unscale()).Start();
                var unscaleSecond = new SmartCoroutine(this,
                    () => scaler.ScaleUp()).Start();
                Chosen = this;

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
                () => Chosen.scaler.Unscale()).Start();

            yield return scaleFirst;
            yield return scaleSecond;
        }
    }
}