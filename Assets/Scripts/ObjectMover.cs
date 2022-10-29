using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField]
    private Vector2 EndPos;
    [SerializeField]
    private float speed;

    private void FixedUpdate() {
        transform.position = Vector2.MoveTowards(transform.position, EndPos, speed);
    }
}