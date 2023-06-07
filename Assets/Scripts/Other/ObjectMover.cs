using System;
using UnityEngine;

namespace Other
{
    public class ObjectMover : MonoBehaviour
    {
        public Vector2 endPos;
        public bool doMove;
        private Vector2 _speed;
        private Action _onEnd;

        public void StartMovement(Vector2 end, float time)
        {
            _speed = (end - (Vector2)transform.position) / time;
            endPos = end;
            doMove = true;
        }
    
        public void StartMovement(Vector2 end, float time, Action action)
        {
            _speed = (end - (Vector2)transform.position) / time;
            endPos = end;
            doMove = true;
            _onEnd = action;
        }
        private void FixedUpdate() {
            if (doMove) {
                transform.position += (_speed * Time.deltaTime).magnitude < (endPos - (Vector2)transform.position).magnitude
                    ? _speed * Time.deltaTime
                    : (Vector3)endPos - transform.position;
            }
            if ((Vector2)transform.position == endPos) {
                doMove = false;
            }
        }
    }
}