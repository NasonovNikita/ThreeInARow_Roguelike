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
        private Vector3 previousScale;
        private Vector3 speed;
        private bool doScale;
        private Action onEnd;
    
        public IEnumerator Scale(Action afterScaleAction = null)
        {
            var localScale = transform.localScale;
            previousScale = localScale;
            onEnd = afterScaleAction;
            speed = (endScale - localScale) / time;
            doScale = true;

            return new WaitUntil(() => !doScale);
        }

        public IEnumerator UnScale(Action afterScaleAction = null)
        {
            endScale = previousScale;
            return Scale(afterScaleAction);
        }
    
        private void FixedUpdate()
        {
            if (doScale)
            {
                transform.localScale += (speed * Time.deltaTime).magnitude < (endScale - transform.localScale).magnitude
                    ? speed * Time.deltaTime
                    : endScale - transform.localScale;
            }

            if (transform.localScale != endScale) return;
            doScale = false;
            onEnd?.Invoke();
            onEnd = null;
        }
    }
}