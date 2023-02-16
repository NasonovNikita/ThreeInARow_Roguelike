using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Vertex: MonoBehaviour, IPointerClickHandler
{
    public Edge prefab;
    
    public List<Vertex> next;

    public Map map;

    public ObjectScaler scaler;

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
        SceneManager.LoadScene("Match3");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(map.OnClick(this));
    }
    
    public void Scale(Vector3 endScale, float time)
    {
        scaler.StartScale(endScale, time);
    }
}