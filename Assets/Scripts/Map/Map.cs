using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public List<Vertex> allVertexes;

    public static int currentVertex = -1;

    public Vector3 baseScale;

    public Vector3 chosenScale;

    public float timeScale;

    public Canvas canvas;

    public GameObject winMessage;

    public void Awake()
    {
        if (currentVertex == allVertexes.Count - 1)
        {
            Win();
        }
        foreach (Vertex vertex in allVertexes)
        {
            vertex.map = this;
        }

        if (currentVertex != -1)
        {
            CurrentVertex_().transform.localScale = chosenScale;
        }
        
        GameManager.instance.SaveData();
    }

    public void OnClick(Vertex vertex)
    {
        if (currentVertex == -1)
        {
            if (allVertexes.IndexOf(vertex) != 0) return;
            
            currentVertex = allVertexes.IndexOf(vertex);
            vertex.ScaleUp(chosenScale, timeScale);
        }
        else if (CurrentVertex_().BelongsToNext(vertex))
        {
            CurrentVertex_().ScaleDown(baseScale, timeScale);
            currentVertex = allVertexes.IndexOf(vertex);
            vertex.ScaleUp(chosenScale, timeScale);
        }
    }

    public Vertex CurrentVertex_()
    {
        return allVertexes[currentVertex];
    }
    
    public void Win()
    {
        GameObject menu = Instantiate(winMessage, canvas.transform, false);
        Button[] buttons = menu.GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(GameManager.instance.NewGame);
        buttons[1].onClick.AddListener(GameManager.instance.Exit);
        menu.gameObject.SetActive(true);
    }
}