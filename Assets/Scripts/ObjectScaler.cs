using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    public Vector3 EndScale = Vector3.one;
    [SerializeField]
    private float ScaleSpeed = 0.02f;
    public bool doScale;

    private void FixedUpdate() {
        if (doScale & EndScale.x > transform.localScale.x) {
            transform.localScale += ((EndScale - transform.localScale).magnitude > (Vector3.one * ScaleSpeed).magnitude) ? Vector3.one * ScaleSpeed: EndScale - transform.localScale;
        }
        else if (doScale & EndScale.x < transform.localScale.x) {
            transform.localScale -= ((transform.localScale - EndScale).magnitude > (Vector3.one * ScaleSpeed).magnitude) ? Vector3.one * ScaleSpeed: transform.localScale - EndScale;
        }
        if (transform.localScale == EndScale) {
            doScale = false;
        }
    }
}