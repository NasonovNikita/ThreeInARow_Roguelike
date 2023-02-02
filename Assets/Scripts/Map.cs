using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Vertex currentVertex;

    public List<Vertex> allVertexes;

    public LineVertex prefab;

    public void Awake()
    {
        foreach (Vertex vertex in allVertexes)
        {
            vertex.map = this;
            vertex.prefab = prefab;
        }
    }

    public void OnClick(Vertex vertex)
    {
        if (currentVertex.BelongsToNext(vertex))
        {
            currentVertex = vertex;
        }
    }
}
