using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    private ObjectMover mover;
    public GemType Type;

    void Awake()
    {
        mover = GetComponent<ObjectMover>();
        mover.doMove = false;
    }

    void Move(Vector2 EndPos) {
        mover.EndPos = EndPos;
        mover.doMove = true;
    }
}