using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    private static List<List<Vertex>> _vertexesPrefabs;

    private List<List<Vertex>> layeredVertexes = new();

    [SerializeField] private List<Vertex> vertexes = new();

    public static int currentVertex = -1;

    public Vector3 baseScale;

    public Vector3 chosenScale;

    public float timeScale;

    public Canvas canvas;

    private MapGenerator generator;

    private GameObject winMessage;

    public void Awake()
    {
        AudioManager.instance.StopAll();

        generator = FindFirstObjectByType<MapGenerator>();

        layeredVertexes = generator.GetMap();

        foreach (var vertex in layeredVertexes.SelectMany(layer => layer))
        {
            vertexes.Add(vertex);
        }

        if (currentVertex == vertexes.Count - 1)
        {
            Win();
        }

        if (currentVertex != -1)
        {
            CurrentVertex_().transform.localScale = chosenScale;
        }
        
        GameManager.instance.SaveData();
        
        AudioManager.instance.Play(AudioEnum.Map);

        winMessage = Resources.Load<GameObject>("Prefabs/Menu/Won");
    }

    public void OnClick(Vertex vertex)
    {
        if (currentVertex == -1)
        {
            if (vertexes.IndexOf(vertex) != 0) return;
            
            currentVertex = vertexes.IndexOf(vertex);
            vertex.ScaleUp(chosenScale, timeScale);
        }
        else if (CurrentVertex_().BelongsToNext(vertex))
        {
            CurrentVertex_().ScaleDown(baseScale, timeScale);
            currentVertex = vertexes.IndexOf(vertex);
            vertex.ScaleUp(chosenScale, timeScale);
        }
    }

    public Vertex CurrentVertex_()
    {
        return vertexes[currentVertex];
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