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
        private Vector3 targetScale;
        private Vector3 originalScale;
        private Vector3 speed;
        private bool doScale;
        private Action onEnd;

        public void Awake() =>
            originalScale = transform.localScale;

        public IEnumerator ScaleUp(Action afterScaleAction = null)
        {
            targetScale = endScale;

            yield return StartCoroutine(Scale(afterScaleAction));
        }

        public IEnumerator UnScale(Action afterScaleAction = null)
        {
            targetScale = originalScale;
            
            yield return StartCoroutine(Scale(afterScaleAction));
        }

        private IEnumerator Scale(Action afterScaleAction)
        {
            onEnd = afterScaleAction;
            speed = (targetScale - transform.localScale) / time;
            doScale = true;

            return new WaitUntil(() => !doScale);
        }
    
        private void FixedUpdate()
        {
            if (doScale)
            {
                transform.localScale += (speed * Time.deltaTime).magnitude < (targetScale - transform.localScale).magnitude
                    ? speed * Time.deltaTime
                    : targetScale - transform.localScale;
            }

            if (transform.localScale != targetScale) return;
            doScale = false;
            onEnd?.Invoke();
            onEnd = null;
        }
    }
}