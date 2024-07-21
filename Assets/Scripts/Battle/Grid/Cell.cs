using System;
using Knot.Localization;
using UnityEngine;

namespace Battle.Grid
{
    [Serializable]
    public abstract class Cell : MonoBehaviour
    {
        [SerializeField] protected KnotTextKeyReference descriptionKeyRef;

        public bool possibleReward;
        [NonSerialized] public bool isInGridBox = false;

        public abstract string Description { get; }

        public abstract bool BoxIsStable(Cell[,] box);
        public abstract bool IsSameType(Cell other);
    }
}