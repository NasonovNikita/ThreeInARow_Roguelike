using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public List<Vertex> allVertexes;

    public static int CurrentVertex = -1;

    public Vector3 baseScale;

    public Vector3 chosenScale;

    public float timeScale;

    public Canvas canvas;

    public GameManager gameManager;

    public GameObject winMessage;

    public void Awake()
    {
        if (CurrentVertex == allVertexes.Count - 1)
        {
            Win();
        }
        foreach (Vertex vertex in allVertexes)
        {
            vertex.map = this;
        }

        if (CurrentVertex != -1)
        {
            CurrentVertex_().transform.localScale = chosenScale;
        }
    }

    public void OnClick(Vertex vertex)
    {
        if (CurrentVertex == -1)
        {
            if (allVertexes.IndexOf(vertex) != 0) return;
            
            CurrentVertex = allVertexes.IndexOf(vertex);
            vertex.Scale(chosenScale, timeScale);
        }
        else if (CurrentVertex_().BelongsToNext(vertex))
        {
            CurrentVertex_().Scale(baseScale, timeScale);
            CurrentVertex = allVertexes.IndexOf(vertex);
            vertex.Scale(chosenScale, timeScale);
        }
    }

    private Vertex CurrentVertex_()
    {
        return allVertexes[CurrentVertex];
    }
    
    public void Win()
    {
        GameObject menu = Instantiate(winMessage, canvas.transform, false);
        Button[] buttons = menu.GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(gameManager.Restart);
        buttons[1].onClick.AddListener(gameManager.Exit);
        menu.gameObject.SetActive(true);
    }
}