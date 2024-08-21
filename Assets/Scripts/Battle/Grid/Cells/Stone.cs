using System.Linq;
using Other;
using UnityEngine;

namespace Battle.Grid.Cells
{
    public class Stone : Cell
    {
        [SerializeField] private int maximumCount;

        public override string Description =>
            descriptionKeyRef.Value.IndexErrorProtectedFormat(maximumCount);

        public override bool BoxIsStable(Cell[,] box)
        {
            var count = Tools.MultiDimToOne(box).Count(IsSameType);
            return 1 <= count && count <= maximumCount;
        }

        public override bool IsSameType(Cell other) => other is Stone;
    }
}