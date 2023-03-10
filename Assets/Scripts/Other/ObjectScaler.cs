using System;
using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    public Vector3 endScale;
    private Vector3 _speed;
    public bool doScale;
    private Action _onEnd;
    
    public void StartScale(Vector3 end, float time)
    {
        _speed = (end - transform.localScale) / time;
        endScale = end;
        doScale = true;
    }
    public void StartScale(Vector3 end, float time, Action action)
    {
        _onEnd = action;
        _speed = (end - transform.localScale) / time;
        endScale = end;
        doScale = true;
    }
    
    private void FixedUpdate()
    {
        if (doScale)
        {
            transform.localScale += (_speed * Time.deltaTime).magnitude < (endScale - transform.localScale).magnitude
                ? _speed * Time.deltaTime
                : endScale - transform.localScale;
        }

        if (transform.localScale != endScale) return;
        doScale = false;
        _onEnd?.Invoke();
        _onEnd = null;
    }
}