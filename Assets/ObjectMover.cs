using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectMover : MonoBehaviour
{
    public Vector2 EndPos;
    [SerializeField]
    private float speed = 0.05f;

    private void FixedUpdate() {
        transform.position = Vector2.MoveTowards(transform.position, EndPos, Math.Min(speed, Mathf.Min(speed, (EndPos - (Vector2) transform.position).magnitude)));
    }
}