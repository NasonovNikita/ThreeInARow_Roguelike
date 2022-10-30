using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GridState State;
    void Start()
    {
        State = GridState.choosing1;
    }

}