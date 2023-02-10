using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Vertex currentVertex;

    public List<Vertex> allVertexes;

    public Vector3 baseScale;

    public Vector3 choosenScale;

    public float timeScale;

    public void Awake()
    {
        foreach (Vertex vertex in allVertexes)
        {
            vertex.map = this;
        }
    }

    public void Start()
    {
        currentVertex.Scale(choosenScale, timeScale);
    }

    public void OnClick(Vertex vertex)
    {
        if (currentVertex.BelongsToNext(vertex))
        {
            currentVertex.Scale(baseScale, timeScale);
            currentVertex = vertex;
            currentVertex.Scale(choosenScale, timeScale);
        }
    }
}