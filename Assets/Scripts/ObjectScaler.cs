using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    public Vector3 endScale;
    [SerializeField]
    private float scaleSpeed = 0.02f;
    public bool doScale;

    private void FixedUpdate()
    {
        if (doScale & endScale.x > transform.localScale.x)
        {
            transform.localScale += (endScale - transform.localScale).magnitude > (Vector3.one * scaleSpeed).magnitude ? Vector3.one * scaleSpeed: endScale - transform.localScale;
        }
        else if (doScale & endScale.x < transform.localScale.x)
        {
            transform.localScale -= (transform.localScale - endScale).magnitude > (Vector3.one * scaleSpeed).magnitude ? Vector3.one * scaleSpeed: transform.localScale - endScale;
        }
        if (transform.localScale == endScale)
        {
            doScale = false;
        }
    }
}