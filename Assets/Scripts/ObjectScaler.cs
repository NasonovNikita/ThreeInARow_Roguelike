using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    public Vector3 endScale;
    private Vector3 _speed;
    public bool doScale;

    public void StartScale(Vector3 end, float time)
    {
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
        if (transform.localScale == endScale)
        {
            doScale = false;
        }
    }
}