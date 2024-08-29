using System.Collections;
using System.Collections.Generic;
using Core.Singleton;
using UnityEngine;

namespace Battle.Grid.Cells.MovingCells
{
    public abstract class MatchingCell : MovingCell
    {
        protected abstract int CountInRow { get; }

        public override bool BoxIsStable(Cell[,] box) =>
            GetCellsToUse().Count == 0;

        /// <summary>
        /// Is called by <see cref="MatchingCellsManager">manager</see> when matched.
        /// </summary>
        public IEnumerator Process()
        {
            yield return StartCoroutine(TimedUse());
            yield return StartCoroutine(OnUsed());
        }

        public abstract List<MatchingCell> GetCellsToUse();

        protected abstract void Use();

        protected virtual IEnumerator TimedUse()
        {
            Use();
            yield return new WaitForSeconds(0.1f);
        }

        protected virtual IEnumerator OnUsed()
        {
            OffScreenPoint.Instance.Hide(gameObject);
            yield break;
        }

        
    }
}