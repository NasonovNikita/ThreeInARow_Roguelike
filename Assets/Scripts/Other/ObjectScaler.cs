using System;
using UnityEngine;

namespace Other
{
    public class ObjectScaler : MonoBehaviour
    {
        public Vector3 endScale;
        private Vector3 speed;
        public float time;
        public bool doScale;
        private Action onEnd;
    
        public void StartScale(Vector3 end, Action action = null)
        {
            onEnd = action;
            speed = (end - transform.localScale) / time;
            endScale = end;
            doScale = true;
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