using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public static KeyValuePair<List<List<VertexData>>, List<List<KeyValuePair<int, int>>>> map;

    private readonly List<List<Vertex>> layeredVertexes = new();

    [SerializeField] private List<Vertex> vertexes = new();

    public static int currentVertex = -1;

    public Vector3 baseScale;
    public Vector3 chosenScale;
    public float timeScale;

    public BattleVertex battlePrefab;
    public ShopVertex shopPrefab;

    public Canvas canvas;

    public void Awake()
    {
        AudioManager.instance.StopAll();

        for (int i = 0; i < map.Key.Count; i++)
        {
            layeredVertexes.Add(new List<Vertex>());
            for (int j = 0; j < map.Key[i].Count; j++)
            {
                Vertex vertex = map.Key[i][j].Type switch
                {
                    VertexType.Battle => ((BattleVertexData)map.Key[i][j]).Init(battlePrefab),
                    VertexType.Shop => ((ShopVertexData)map.Key[i][j]).Init(shopPrefab),
                    _ => throw new ArgumentOutOfRangeException()
                };
                layeredVertexes[i].Add(vertex);
                vertexes.Add(vertex);
            }
        }

        for (int i = 0; i < map.Value.Count; i++)
        {
            var oldLayer = layeredVertexes[i];
            var newLayer = layeredVertexes[i + 1];
            foreach (var bounds in map.Value[i])
            {
                oldLayer[bounds.Key].next.Add(newLayer[bounds.Value]);
            }
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
        GameObject menu = Instantiate(PrefabsContainer.instance.winMessage, canvas.transform, false);
        var buttons = menu.GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(GameManager.instance.NewGame);
        buttons[1].onClick.AddListener(GameManager.instance.MainMenu);
        menu.gameObject.SetActive(true);
    }
}