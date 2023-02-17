using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public List<Vertex> allVertexes;

    public static int CurrentVertex = -1;

    public Vector3 baseScale;

    public Vector3 chosenScale;

    public float timeScale;

    public void Awake()
    {
        foreach (Vertex vertex in allVertexes)
        {
            vertex.map = this;
        }

        if (CurrentVertex != -1)
        {
            CurrentVertex_().transform.localScale = chosenScale;
        }
    }

    public IEnumerator<WaitForSeconds> OnClick(Vertex vertex)
    {
        if (CurrentVertex == -1)
        {
            if (allVertexes.IndexOf(vertex) != 0) yield break;
            
            CurrentVertex = allVertexes.IndexOf(vertex);
            vertex.Scale(chosenScale, timeScale);
            yield return new WaitForSeconds(timeScale);
            vertex.OnArrive();
        }
        else if (CurrentVertex_().BelongsToNext(vertex))
        {
            CurrentVertex_().Scale(baseScale, timeScale);
            CurrentVertex = allVertexes.IndexOf(vertex);
            vertex.Scale(chosenScale, timeScale);
            yield return new WaitForSeconds(timeScale);
            vertex.OnArrive();
        }
    }

    private Vertex CurrentVertex_()
    {
        return allVertexes[CurrentVertex];
    }
}