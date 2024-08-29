using System;
using System.Collections;
using Knot.Localization;
using Other;
using UnityEngine;

namespace Battle.Grid
{
    /// A cell is an object stored in Grid.
    [Serializable]
    public abstract class Cell : MonoBehaviour
    {
        [SerializeField] protected ObjectMover mover;
        [SerializeField] protected ObjectScaler scaler;
        
        [SerializeField] protected KnotTextKeyReference descriptionKeyRef;

        /// Defines if it can be dropped as a reward in a battle.
        public bool possibleReward;

        [NonSerialized] public bool IsInGridBox = false;      
        
        protected IEnumerator SwitchCells(Cell first, Cell second)
        {
            var moveFirst = new SmartCoroutine(this,
                () => first.mover.MoveTo(second.transform.position));
            var moveSecond = new SmartCoroutine(this,
                () => second.mover.MoveTo(first.transform.position));

            moveFirst.Start();
            moveSecond.Start();

            yield return moveFirst;
            yield return moveSecond;
        }

        public abstract string Description { get; }

        public abstract bool BoxIsStable(Cell[,] box);
        public abstract bool IsSameType(Cell other);
    }
}