using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField]
    private MapData mapData;
    
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
        LoadMapPos();
    }

    public void Start()
    {
        currentVertex.Scale(choosenScale, timeScale);
    }

    public IEnumerator<WaitForSeconds> OnClick(Vertex vertex)
    {
        if (currentVertex.BelongsToNext(vertex))
        {
            currentVertex.Scale(baseScale, timeScale);
            currentVertex = vertex;
            SaveMapPos();
            currentVertex.Scale(choosenScale, timeScale);
            yield return new WaitForSeconds(timeScale);
            currentVertex.OnArrive();
        }
    }
    
    public void SaveMapPos()
    {
        mapData.currentVertex = currentVertex;
    }

    public void LoadMapPos()
    {
        currentVertex = mapData.currentVertex;
    }
}