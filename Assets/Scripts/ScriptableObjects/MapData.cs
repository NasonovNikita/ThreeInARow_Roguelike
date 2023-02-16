using UnityEngine;

[CreateAssetMenu(fileName = "MaoData", menuName = "Map Data", order = 52)]
public class MapData : ScriptableObject
{
    public Vertex currentVertex;
    public Vertex baseCurrentVertex;

    public void Reset()
    {
        currentVertex = baseCurrentVertex;
    }
}
