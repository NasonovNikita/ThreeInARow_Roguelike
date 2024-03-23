using System;
using UnityEngine;

namespace UI.Battle
{
    public class HUDSpawner : MonoBehaviour
    {
        [SerializeField] private HUD hudPrefab;

        public void CreateHUD(string content, Color color, Direction direction = Direction.None)
        {
            var hud = Instantiate(hudPrefab);
            hud.text.text = content;
            hud.text.color = color;
            switch (direction)
            {
                case Direction.Up:
                    hud.MoveUp();
                    break;
                case Direction.Down:
                    hud.MoveDown();
                    break;
                case Direction.None:
                    hud.Stay();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
    }

    public enum Direction
    {
        Up,
        Down,
        None
    }
}