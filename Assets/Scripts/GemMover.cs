using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemMover : MonoBehaviour
{
    public ObjectMover ObjectMover;
    public Vector2 target;
    private void SetTarget(Vector2 target) {
        ObjectMover.EndPos = target;
    }

    void Update(){
        SetTarget(target);
    }
}