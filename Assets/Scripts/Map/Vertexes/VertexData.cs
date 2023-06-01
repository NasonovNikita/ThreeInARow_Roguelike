using System;
using UnityEngine;

[Serializable]
public abstract class VertexData
{
    public Vector3 position;

    public abstract VertexType Type { get; }
}
