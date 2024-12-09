using System;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI
{
    /// <summary>
    ///     Resizes Grid to fit cells correctly
    /// </summary>
    public class GridResizer : MonoBehaviour
    {
        public static GridResizer Instance { get; private set; }
        
        [SerializeField] private Grid.Grid grid;
        [SerializeField] private GridLayoutGroup layout;

        public void Awake()
        {
            Instance = this;
        }

        public void Resize()
        {
            // Sizes required to fit cells
            var x = (int)((layout.spacing.x + layout.cellSize.x) * grid.sizeX);
            var y = (int)((layout.spacing.y + layout.cellSize.y) * grid.sizeY);

            var sizeDelta = grid.GetComponent<RectTransform>().sizeDelta;

            if (sizeDelta.x < x || sizeDelta.y < y) // If cells don't fit shrink cells
            {
                var cellPart =
                    layout.cellSize.x / (layout.cellSize.x + layout.spacing.x);
                var spacingPart =
                    layout.spacing.x / (layout.cellSize.x + layout.spacing.x);
                // Sum size of cell and space to fit
                var cellAndSpaceSize = sizeDelta.x / Math.Max(grid.sizeX, grid.sizeY);

                // Using x because until resized Grid is square. 
                var newCellSize = (int)(cellPart * cellAndSpaceSize);
                var newSpacingSize = (int)(spacingPart * cellAndSpaceSize);

                layout.cellSize = new Vector2(newCellSize, newCellSize);
                layout.spacing = new Vector2(newSpacingSize, newSpacingSize);


                x = (int)((layout.spacing.x + layout.cellSize.x) * grid.sizeX);
                y = (int)((layout.spacing.y + layout.cellSize.y) * grid.sizeY);
            }

            grid.GetComponent<RectTransform>().sizeDelta = new Vector2(x, y);
        }
    }
}