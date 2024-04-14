using System;
using UnityEngine;

namespace UI.Battle.HUD
{
    public class HUDSpawner : MonoBehaviour
    {
        public void SpawnHUD(string content, Color color, HUDMoveDirection hudMoveDirection = HUDMoveDirection.None)
        {
            var hud = HUD.Create(content, color, transform);
            
            MoveHUD(hud, hudMoveDirection);
        }

        private static void MoveHUD(HUD hud, HUDMoveDirection direction)
        {
            switch (direction)
            {
                case HUDMoveDirection.Up:
                    hud.MoveUp();
                    break;
                case HUDMoveDirection.Down:
                    hud.MoveDown();
                    break;
                case HUDMoveDirection.None:
                    hud.Stay();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
    }
}