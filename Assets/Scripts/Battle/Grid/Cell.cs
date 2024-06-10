using System;
using Knot.Localization;
using UnityEngine;

namespace Battle.Grid
{
    [Serializable]
    public abstract class Cell : MonoBehaviour
    {
        [NonSerialized] public bool isInGridBox = false;
        [SerializeField] protected KnotTextKeyReference descriptionKeyRef;

        public bool possibleReward;
        
        public abstract bool BoxIsStable(Cell[,] box);
        public abstract bool IsSameType(Cell other);
        
        public abstract string Description { get; }
    }
}