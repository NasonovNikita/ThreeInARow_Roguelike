using System;
using UnityEngine;

namespace Other
{
    public class ObjectMover : MonoBehaviour
    {
        public Vector2 endPos;
        public bool doMove;
        public float time;
        public SpeedType speedType;
        private Vector2 speed;
        private Vector2 acceleration;
        private Action onEnd;
    
        public void StartMovementTo(Vector2 end, Action action = null)
        {
            speed = (end - (Vector2)transform.position) / time;
            endPos = end;
            doMove = true;
            onEnd = action;
        }

        public void StartMovementBy(Vector2 delta, Action action = null)
        {
            switch (speedType)
            {
                case SpeedType.Const:
                    speed = delta / time;
                    acceleration = Vector2.zero;
                    break;
                case SpeedType.LinearDecrease:
                    speed = delta * 2 / time;
                    acceleration = -2 * delta / (float)Math.Pow(time, 2);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            endPos = (Vector2)transform.position + delta;
            doMove = true;
            onEnd = action;
        }


        private void FixedUpdate() {
            if (doMove) {
                transform.position += (speed * Time.deltaTime).magnitude < (endPos - (Vector2)transform.position).magnitude
                    ? speed * Time.deltaTime
                    : (Vector3)endPos - transform.position;
                speed += acceleration * Time.deltaTime;
            }

            if ((Vector2)transform.position != endPos) return;
            doMove = false;
            onEnd?.Invoke();
        }
    }

    public enum SpeedType
    {
        Const,
        LinearDecrease
    }
}