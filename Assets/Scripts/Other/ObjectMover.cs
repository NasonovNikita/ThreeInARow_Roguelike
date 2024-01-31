using System;
using UnityEngine;

namespace Other
{
    public class ObjectMover : MonoBehaviour
    {
        public Vector2 endPos;
        public bool doMove;
        public float time;
        private Vector2 speed;
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
            speed = delta / time;
            endPos = (Vector2)transform.position + delta;
            doMove = true;
            onEnd = action;
        }


        private void FixedUpdate() {
            if (doMove) {
                transform.position += (speed * Time.deltaTime).magnitude < (endPos - (Vector2)transform.position).magnitude
                    ? speed * Time.deltaTime
                    : (Vector3)endPos - transform.position;
            }

            if ((Vector2)transform.position != endPos) return;
            doMove = false;
            onEnd?.Invoke();
        }
    }
}