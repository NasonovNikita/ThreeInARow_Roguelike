using System;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI
{
    public class GridResizer : MonoBehaviour
    {
        [SerializeField] private Grid.Grid grid;
        [SerializeField] private GridLayoutGroup layout;

        public void Start()
        {
            // grid is square

            var x = (int)((layout.spacing.x + layout.cellSize.x) * grid.sizeX);
            var y = (int)((layout.spacing.y + layout.cellSize.y) * grid.sizeY);

            Vector2 sizeDelta = grid.GetComponent<RectTransform>().sizeDelta;


            if (sizeDelta.x < x || sizeDelta.y < y)
            {
                var maxSize = Math.Max(grid.sizeX, grid.sizeY);

                var newCellSize =
                    (int)(layout.cellSize.x / (layout.cellSize.x + layout.spacing.x) * sizeDelta.x /
                          maxSize);
                var newSpacingSize =
                    (int)(layout.spacing.x / (layout.cellSize.x + layout.spacing.x) * sizeDelta.x /
                          maxSize);

                layout.cellSize = new Vector2(newCellSize, newCellSize);
                layout.spacing = new Vector2(newSpacingSize, newSpacingSize);
            }
            else
            {
                grid.GetComponent<RectTransform>().sizeDelta = new Vector2(x, y);
            }
        }
    }
}