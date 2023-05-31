using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Vertex: MonoBehaviour, IPointerClickHandler
{
    private Edge prefab;
    
    public List<Vertex> next;

    public VertexType type;

    private Map map;

    public void Awake()
    {
        prefab = Resources.Load<Edge>("Prefabs/Map/Edge");
        map = FindFirstObjectByType<Map>();
    }

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

    public virtual void OnArrive()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        map.OnClick(this);
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    public void ScaleUp(Vector3 endScale, float time)
    {
        ObjectScaler scaler = GetComponent<ObjectScaler>();
        scaler.StartScale(endScale, time, OnArrive);
    }
    
    public void ScaleDown(Vector3 endScale, float time)
    {
        ObjectScaler scaler = GetComponent<ObjectScaler>();
        scaler.StartScale(endScale, time);
    }
}