using System;
using Battle.Units.Stats;
using UnityEngine;

namespace UI.Battle.HUD
{
    [Serializable]
    public struct StatHUDData
    {
        [SerializeReference] public Stat stat;
            
        [SerializeField] private Color increaseColor;
        [SerializeField] private Color decreaseColor;
        [SerializeField] private Color defaultColor;

        public Color ColorByStatValueChange(int delta) =>
            delta switch
            {
                > 0 => increaseColor,
                0 => defaultColor,
                < 0 => decreaseColor
            };

        public HUDMoveDirection HUDMoveDirectionByStatValueChange(int delta) =>
            delta switch
            {
                > 0 => HUDMoveDirection.Up,
                0 => HUDMoveDirection.None,
                < 0 => HUDMoveDirection.Down
            };
    }
}