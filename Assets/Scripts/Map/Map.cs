using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public static List<List<Vertex>> vertexesPrefabs = new();

    private readonly List<Vertex> allVertexes = new();

    public static int currentVertex = -1;

    public Vector3 baseScale;

    public Vector3 chosenScale;

    public float timeScale;

    public Canvas canvas;

    public GameObject winMessage;

    public void Start()
    {
        AudioManager.instance.StopAll();

        for (int i = 0; i < vertexesPrefabs.Count; i++)
        {
            for (int j = 0; j < vertexesPrefabs[i].Count; j++)
            {
                Vertex vertex = Instantiate(vertexesPrefabs[i][j]);
                vertex.transform.position = transform.position + Vector3.up * i * 3 + Vector3.right * j * 3;
                allVertexes.Add(vertex);
            }
        }

        if (currentVertex == allVertexes.Count - 1)
        {
            Win();
        }

        if (currentVertex != -1)
        {
            CurrentVertex_().transform.localScale = chosenScale;
        }
        
        GameManager.instance.SaveData();
        
        AudioManager.instance.Play(AudioEnum.Map);

        winMessage = Resources.Load<GameObject>("Prefabs/Menu/Win");
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

    private void Win()
    {
        GameObject menu = Instantiate(winMessage, canvas.transform, false);
        Button[] buttons = menu.GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(GameManager.instance.NewGame);
        buttons[1].onClick.AddListener(GameManager.instance.MainMenu);
        menu.gameObject.SetActive(true);
    }
}