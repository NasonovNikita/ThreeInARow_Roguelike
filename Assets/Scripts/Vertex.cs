using System.Collections.Generic;
using UnityEngine;

public class Vertex: MonoBehaviour
{
    [SerializeField]
    private List<Vertex> next;

    public Map map;



    public bool BelongsToNext(Vertex vertex)
    {
        return next.Contains(vertex);
    }

    public void AddNext(Vertex nextVertex)
    {
        next.Add(nextVertex);
    }

    public void OnClick()
    {
        map.OnClick(this);
    }
}