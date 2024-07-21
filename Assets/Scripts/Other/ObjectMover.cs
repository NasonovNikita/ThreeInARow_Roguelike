using System;
using System.Collections;
using UnityEngine;

namespace Other
{
    public class ObjectMover : MonoBehaviour
    {
        public Vector3 endPos;
        public bool doMove;
        public float time;
        public SpeedType speedType;
        private Vector3 acceleration;
        private Action onEnd;
        private Vector3 speed;


        private void FixedUpdate()
        {
            if (doMove)
            {
                transform.position +=
                    (speed * Time.deltaTime).magnitude < (endPos - transform.position).magnitude
                        ? speed * Time.deltaTime
                        : endPos - transform.position;
                speed += acceleration * Time.deltaTime;
            }

            if (transform.position != endPos) return;
            doMove = false;
            onEnd?.Invoke();
        }

        public IEnumerator MoveTo(Vector3 end, Action doAfterMove = null)
        {
            Vector3 delta = end - transform.position;
            return MoveBy(delta, doAfterMove);
        }

        public IEnumerator MoveBy(Vector3 delta, Action doAfterMove = null)
        {
            SetSpeedAcceleration(delta);
            endPos = transform.position + delta;
            doMove = true;
            onEnd = doAfterMove;

            return new WaitUntil(() => !doMove);
        }

        private void SetSpeedAcceleration(Vector3 delta)
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
        }
    }

    public enum SpeedType
    {
        Const,
        LinearDecrease
    }
}