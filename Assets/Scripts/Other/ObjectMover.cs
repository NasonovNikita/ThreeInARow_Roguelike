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
        [SerializeField] private Vector3 endPos;
        [SerializeField] private float time;
        [SerializeField] private SpeedType speedType;

        private Vector3 _acceleration;
        private Vector3 _speed;

        public IEnumerator MoveTo(Vector3 endPosition)
        {
            endPos = endPosition;
            var delta = endPosition - transform.position;

            yield return StartCoroutine(MoveBy(delta));
        }

        public IEnumerator MoveBy(Vector3 delta)
        {
            SetSpeedAndAcceleration(delta);

            yield return StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            var startTime = Time.time;

            while ((transform.position - endPos).magnitude >
                   (_speed * Time.deltaTime).magnitude && startTime + time > Time.time)
            {
                transform.position += _speed * Time.deltaTime;
                _speed += _acceleration * Time.deltaTime;

                yield return new WaitForFixedUpdate();
            }

            transform.position = endPos;
        }

        private void SetSpeedAndAcceleration(Vector3 delta)
        {
            switch (speedType)
            {
                case SpeedType.Const:
                    _speed = delta / time;
                    _acceleration = Vector3.zero;
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