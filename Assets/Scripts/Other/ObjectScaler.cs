using System;
using System.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Other
{
    public class ObjectScaler : MonoBehaviour
    {
        [SerializeField] private Vector3 endScale;
        [SerializeField] private float time;
        private bool _doScale;
        private Action _onEnd;
        private Vector3 _originalScale;
        private Vector3 _speed;
        private Vector3 _targetScale;

        public void Awake()
        {
            _originalScale = transform.localScale;
        }

        private void FixedUpdate()
        {
            if (_doScale)
                transform.localScale += (_speed * Time.deltaTime).magnitude <
                                        (_targetScale - transform.localScale).magnitude
                    ? _speed * Time.deltaTime
                    : _targetScale - transform.localScale;

            if (transform.localScale != _targetScale) return;
            _doScale = false;
            _onEnd?.Invoke();
            _onEnd = null;
        }

        /// <summary>
        ///     Scales to <see cref="endScale"/>
        /// </summary>
        public IEnumerator ScaleUp(Action afterScaleAction = null)
        {
            _targetScale = endScale;

            yield return StartCoroutine(Scale(afterScaleAction));
        }

        /// <summary>
        ///     Scales object to the scale the object had
        ///     when <see cref="Awake"/> was called.
        /// </summary>
        public IEnumerator UnScale(Action afterScaleAction = null)
        {
            _targetScale = _originalScale;

            yield return StartCoroutine(Scale(afterScaleAction));
        }

        private IEnumerator Scale(Action afterScaleAction)
        {
            _onEnd = afterScaleAction;
            _speed = (_targetScale - transform.localScale) / time;
            _doScale = true;

            return new WaitUntil(() => !_doScale);
        }
    }
}