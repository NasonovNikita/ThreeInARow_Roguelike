using System.Collections;
using Core.Singleton;
using UnityEngine;

namespace Battle.Grid.Cells.MovingCells
{
    public abstract class MatchingCell : MovingCell
    {
        public override bool BoxIsStable(Cell[,] box) =>
            MatchingCellsManager.Instance.FindRowedCells().Count == 0;

        protected sealed override void OnMoveDone()
        {
            MatchingCellsManager.Instance.Process();
        }

        public IEnumerator Process()
        {
            yield return StartCoroutine(TimedUse());
            OffScreenPoint.Instance.Hide(gameObject);
        }

        protected abstract void Use();

        protected virtual IEnumerator TimedUse()
        {
            Use();
            yield return new WaitForSeconds(0.1f);
        }
    }
}