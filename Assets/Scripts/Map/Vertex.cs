using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Vertex: MonoBehaviour, IPointerClickHandler
{
    public Edge prefab;
    
    public List<Vertex> next;

    public Map map;

    public void Start()
    {
        foreach (Vertex vertex in next)
        {
            Edge edge = Instantiate(prefab);
            edge.Draw(transform.position, vertex.transform.position);
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

    public void OnArrive()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
        OnArrive();
    }
}