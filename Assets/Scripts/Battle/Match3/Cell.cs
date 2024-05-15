using System;
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
            grid = Grid.Instance;
            generator = GridGenerator.Instance;
        }

        public abstract bool BoxIsStable(Cell[,] box);
        public abstract bool IsSameType(Cell second);

        public void Delete() => CellPool.Instance.Release(this);
    }
}