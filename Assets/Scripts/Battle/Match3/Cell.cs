using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Match3
{
    [Serializable]
    public abstract class Cell : MonoBehaviour
    {
        protected Grid grid;
        protected GridGenerator generator;

        public virtual void Awake()
        {
            grid = FindFirstObjectByType<Grid>();
            generator = FindFirstObjectByType<GridGenerator>();
        }

        public abstract bool BoxIsStable { get; }

        public virtual List<(Func<IEnumerator>, int)> PossibleActions()
        {
            return new List<(Func<IEnumerator>, int)> { (null, 0) };
        }
    }
}