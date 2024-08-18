using System;
using System.Collections;
using UnityEngine;

namespace Other
{
    /// <summary>
    ///     Moves object by time.
    /// </summary>
    public class ObjectMover : MonoBehaviour
    {
        public Vector3 endPos;
        public float time;
        public SpeedType speedType;
        private Vector3 _acceleration;
        private bool _doMove;
        private Action _onEnd;
        private Vector3 _speed;


        private void FixedUpdate()
        {
            if (_doMove)
            {
                transform.position +=
                    (_speed * Time.deltaTime).magnitude <
                    (endPos - transform.position).magnitude
                        ? _speed * Time.deltaTime
                        : endPos - transform.position;
                _speed += _acceleration * Time.deltaTime;
            }

            if (transform.position == endPos && _doMove) return;
            _doMove = false;
            _onEnd?.Invoke();
        }

        public IEnumerator MoveTo(Vector3 end, Action doAfterMoved = null)
        {
            var delta = end - transform.position;
            return MoveBy(delta, doAfterMoved);
        }

        public IEnumerator MoveBy(Vector3 delta, Action doAfterMoved = null)
        {
            SetSpeedAcceleration(delta);
            endPos = transform.position + delta;
            _doMove = true;
            _onEnd = doAfterMoved;

            return new WaitUntil(() => !_doMove);
        }

        private void SetSpeedAcceleration(Vector3 delta)
        {
            switch (speedType)
            {
                case SpeedType.Const:
                    _speed = delta / time;
                    _acceleration = Vector2.zero;
                    break;
                case SpeedType.LinearDecrease:
                    _speed = delta * 2 / time;
                    _acceleration = -2 * delta / (float)Math.Pow(time, 2);
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