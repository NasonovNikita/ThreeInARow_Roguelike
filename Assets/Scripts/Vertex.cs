using System.Collections.Generic;
using UnityEngine;

public class Vertex: MonoBehaviour
{
    public LineVertex prefab;
    
    public List<Vertex> next;

    public Map map;

    public void Start()
    {
        foreach (Vertex vertex in next)
        {
            LineVertex newVertex = Instantiate(prefab);
            newVertex.transform.position = transform.position;
            newVertex.Draw(vertex.transform.position);
        }
    }

    public bool BelongsToNext(Vertex vertex)
    {
        return next.Contains(vertex);
    }

    public void OnClick()
    {
        map.OnClick(this);
    }
}