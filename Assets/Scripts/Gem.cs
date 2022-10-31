using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    private ObjectMover mover;
    private ObjectScaler scaler;
    public GemType Type;
    public Grid grid;

    private void Awake()
    {
        mover = GetComponent<ObjectMover>();
        mover.doMove = false;
        scaler = GetComponent<ObjectScaler>();
        scaler.doScale = false;
    }

    void Move(Vector2 EndPos) {
        mover.EndPos = EndPos;
        mover.doMove = true;
    }

    void Scale(Vector3 EndScale) {
        scaler.EndScale = EndScale;
        scaler.doScale = true;
    }
}