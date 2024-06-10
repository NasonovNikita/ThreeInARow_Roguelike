using System;
using UnityEngine;
using UnityEngine.UI;
using Grid = Battle.Grid.Grid;

namespace UI.Battle
{
    public class GridResizer : MonoBehaviour
    {
        [SerializeField] private Grid grid;
        [SerializeField] private GridLayoutGroup layout;

        public void Start()
        {
            // grid is square
            
            int x = (int)((layout.spacing.x + layout.cellSize.x) * grid.sizeX);
            int y = (int)((layout.spacing.y + layout.cellSize.y) * grid.sizeY);

            var sizeDelta = grid.GetComponent<RectTransform>().sizeDelta;

            
            if (sizeDelta.x < x || sizeDelta.y < y)
            {
                int maxSize = Math.Max(grid.sizeX, grid.sizeY);

                int newCellSize =
                    (int)(layout.cellSize.x / (layout.cellSize.x + layout.spacing.x) * sizeDelta.x / maxSize);
                int newSpacingSize =
                    (int)(layout.spacing.x / (layout.cellSize.x + layout.spacing.x) * sizeDelta.x / maxSize);

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