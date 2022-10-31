using UnityEngine;
using System;

public class ObjectMover : MonoBehaviour
{
    public Vector2 endPos;
    public bool doMove;
    private float speed = 0.05f;

    private void FixedUpdate() {
        if (doMove) {
            transform.position = Vector2.MoveTowards(transform.position, endPos, Math.Min(speed, Mathf.Min(speed, (endPos - (Vector2) transform.position).magnitude)));
        }
        if ((Vector2)transform.position == endPos) {
            doMove = false;
        }
    }
}