using System;
using Knot.Localization;
using UnityEngine;

namespace Battle.Grid
{
    /// A cell is an object stored in Grid.
    [Serializable]
    public abstract class Cell : MonoBehaviour
    {
        [SerializeField] protected KnotTextKeyReference descriptionKeyRef;

        /// Defines if it can be dropped as a reward in a battle.
        public bool possibleReward;

        [NonSerialized] public bool IsInGridBox = false;

        public abstract string Description { get; }

        public abstract bool BoxIsStable(Cell[,] box);
        public abstract bool IsSameType(Cell other);
    }
}