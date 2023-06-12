using System;
using UnityEngine;

namespace Map.Vertexes
{
    [Serializable]
    public abstract class VertexData
    {
        public Vector3 position;

        public abstract VertexType Type { get; }
    }
}