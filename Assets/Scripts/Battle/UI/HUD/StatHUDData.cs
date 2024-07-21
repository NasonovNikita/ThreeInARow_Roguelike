using System;
using UnityEngine;

namespace Battle.UI.HUD
{
    [Serializable]
    public struct StatHUDData
    {
        [SerializeField] private Color increaseColor;
        [SerializeField] private Color decreaseColor;
        [SerializeField] private Color defaultColor;

        public Color ColorByStatValueChange(int delta)
        {
            return delta switch
            {
                > 0 => increaseColor,
                0 => defaultColor,
                < 0 => decreaseColor
            };
        }

        public HUDMoveDirection HUDMoveDirectionByStatValueChange(int delta)
        {
            return delta switch
            {
                > 0 => HUDMoveDirection.Up,
                0 => HUDMoveDirection.None,
                < 0 => HUDMoveDirection.Down
            };
        }
    }
}