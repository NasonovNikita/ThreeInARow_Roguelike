using System.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Other
{
    public class ObjectScaler : MonoBehaviour
    {
        [SerializeField] private Vector3 endScale;
        [SerializeField] private float time;
        private Vector3 _originalScale;
        private Vector3 _speed;
        private Vector3 _targetScale;

        public void Awake()
        {
            _originalScale = transform.localScale;
        }

        /// <summary>
        ///     Scales to <see cref="endScale"/>
        /// </summary>
        public IEnumerator ScaleUp()
        {
            _targetScale = endScale;

            yield return StartCoroutine(Scale());
        }

        /// <summary>
        ///     Scales object to the scale the object had
        ///     when <see cref="Awake"/> was called.
        /// </summary>
        public IEnumerator Unscale()
        {
            _targetScale = _originalScale;

            yield return StartCoroutine(Scale());
        }

        private IEnumerator Scale()
        {
            _speed = (_targetScale - transform.localScale) / time;

            var startTime = Time.time;

            while ((transform.localScale - _targetScale).magnitude >
                   (_speed * Time.deltaTime).magnitude && startTime + time > Time.time)
            {
                transform.localScale += _speed * Time.deltaTime;

                yield return new WaitForFixedUpdate();
            }

            transform.localScale = _targetScale;
        }
    }
}