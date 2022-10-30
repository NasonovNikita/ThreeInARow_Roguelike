using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int size = 10;
    [SerializeField]
    private Vector2 position;

    public GridState State;
    void Awake()
    {
        State = GridState.choosing1;
        transform.position = position;
    }

}