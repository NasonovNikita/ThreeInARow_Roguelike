using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public List<Vertex> allVertexes;

    private static int _currentVertex = -1;

    public Vector3 baseScale;

    public Vector3 chosenScale;

    public float timeScale;

    public void Awake()
    {
        foreach (Vertex vertex in allVertexes)
        {
            vertex.map = this;
        }

        if (_currentVertex != -1)
        {
            CurrentVertex().transform.localScale = chosenScale;
        }
    }

    public IEnumerator<WaitForSeconds> OnClick(Vertex vertex)
    {
        if (_currentVertex == -1)
        {
            _currentVertex = allVertexes.IndexOf(vertex);
            vertex.Scale(chosenScale, timeScale);
            yield return new WaitForSeconds(timeScale);
            vertex.OnArrive();
        }
        else if (CurrentVertex().BelongsToNext(vertex))
        {
            CurrentVertex().Scale(baseScale, timeScale);
            _currentVertex = allVertexes.IndexOf(vertex);
            vertex.Scale(chosenScale, timeScale);
            yield return new WaitForSeconds(timeScale);
            vertex.OnArrive();
        }
    }

    private Vertex CurrentVertex()
    {
        return allVertexes[_currentVertex];
    }
}