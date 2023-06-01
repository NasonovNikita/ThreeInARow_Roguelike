using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private BattleVertex battlePrefab;
    [SerializeField] private ShopVertex shopPrefab;
    
    public static int seed;

    public int difficulty;
    public int depth;
    public int minWidth;
    public int maxWidth;
    
    
    [SerializeField] private GameObject up;
    [SerializeField] private GameObject down;
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject right;

    private readonly Vector3 dX = new (0.01f, 0, 0);
    private readonly Vector3 dY = new(0, 0.01f, 0);

    private readonly Dictionary<int, List<EnemyGroup>> groups = new();

    private readonly Dictionary<VertexType, int> vertexesByChance = new ()
    {
        [VertexType.Battle] = 10,
        [VertexType.Shop] = 3
    };
    private int vertexesFrequencySum;

    private Good[] goods;
    private int goodsFrequencySum;
    
    public List<List<Vertex>> GetMap()
    {
        Random.InitState(seed);

        foreach (EnemyGroup group in Resources.LoadAll<EnemyGroup>("Presets/EnemyGroups"))
        {
            if (!groups.TryAdd(group.Difficulty(), new List<EnemyGroup> {group}))
            {
                groups[group.Difficulty()].Add(group);
            }
        }

        goods = Resources.LoadAll<Good>("Goods");
        foreach (Good good in goods)
        {
            goodsFrequencySum += good.frequency;
        }

        foreach (var vertex in vertexesByChance)
        {
            vertexesFrequencySum += vertex.Value;
        }

        List<List<Vertex>> layers =  Generate();
        
        PlaceVertexes(layers);
        GoodsPricing(layers);
        BindLayers(layers);

        
        Random.InitState((int) DateTime.Now.Ticks);
        return layers;
    }

    private List<List<Vertex>> Generate()
    {
        List<List<Vertex>> layers = new() { new List<Vertex> {GenBattle(0)} };

        for (int i = 1; i < depth - 1; i++)
        {
            layers.Add(GenLayer(i));
        }

        layers.Add(new List<Vertex> {GenBattle(depth)});

        return layers;
    }

    private void PlaceVertexes(List<List<Vertex>> layers)
    {
        Vector3 yStep = (up.transform.position - down.transform.position) / (layers.Count - 1);
        int width = layers.Max(layer => layer.Count);
        Vector3 xStep = (left.transform.position - right.transform.position) / width;
        for (int i = 0; i < layers.Count; i++)
        {
            for (int j = 0; j < layers[i].Count; j++)
            {
                PlaceLayer(layers[i], i, xStep, yStep);
            } 
        }
    }

    private void PlaceLayer(List<Vertex> layer, int i, Vector3 xStep, Vector3 yStep)
    {
        int k = layer.Count;
        layer[0].transform.position = down.transform.position + i * yStep + ((float) k - 1) / 2 * -xStep;
        for (int j = 1; j < k; j++)
        {
            layer[j].transform.position = layer[j - 1].transform.position + xStep + Random.Range(-25, 26) * dX + Random.Range(-25, 26) * dY;
        }
    }

    private List<Vertex> GenLayer(int layer)
    {
        List<Vertex> resultLayer = new();

        int width = Random.Range(minWidth, maxWidth + 1);

        for (int i = 0; i < width; i++)
        {
            resultLayer.Add(ChooseVertex(layer));
        }

        return resultLayer;
    }

    private void BindLayers(List<List<Vertex>> layers)
    {
        for (int i = 0; i < depth - 1; i++)
        {
            Bind2Layers(layers[i], layers[i + 1]);
        }
    }

    private void Bind2Layers(List<Vertex> oldLayer, List<Vertex> newLayer)
    {
        HashSet<Vertex> boundVertexes = new();

        List<KeyValuePair<int, int>> bounds = new();

        while (boundVertexes.Count != oldLayer.Count + newLayer.Count)
        {
            for (int i = 0; i < oldLayer.Count; i++)
            {
                for (int j = 0; j < newLayer.Count; j++)
                {
                    if (Random.Range(0, 2) == 0 || CrossExists(bounds, i, j) ||
                        oldLayer[i].next.Contains(newLayer[j])) continue;
                    oldLayer[i].next.Add(newLayer[j]);
                    bounds.Add(new KeyValuePair<int, int>(i, j));
                    boundVertexes.AddRange(new[] { oldLayer[i], newLayer[j] });
                }
            }
        }
    }

    private BattleVertex GenBattle(int layer)
    {
        BattleVertex vertex = Instantiate(battlePrefab);

        int battleDifficulty = difficulty + layer * difficulty / 2 + Random.Range(-5, 6);
        int chosenKey = groups.Keys.Aggregate(
                (min, next) => Math.Abs(min - battleDifficulty) < Math.Abs(next - battleDifficulty) ? min : next
                );

        
        EnemyGroup group = groups[chosenKey][Random.Range(0, groups[chosenKey].Count)];
        vertex.enemies = group.GetEnemies();
        
        return vertex;
    }

    private ShopVertex GenShop()
    {
        ShopVertex vertex = Instantiate(shopPrefab);

        List<Good> currentGoods = new();

        for (int i = 0; i < 4; i++)
        {
            currentGoods.Add(ChooseGood());
        }

        vertex.goods = currentGoods;

        return vertex;
    }

    private void GoodsPricing(List<List<Vertex>> layers)
    {
        for (int i = 0; i < layers.Count; i++)
        {
            for (int j = 0; j < layers[i].Count; j++)
            {
                if (layers[i][j].type != VertexType.Shop) continue;
                List<Good> localGoods = new();
                foreach (var goodCopy in ((ShopVertex)layers[i][j]).goods.Select(Instantiate))
                {
                    goodCopy.price = (int) (goodCopy.price * (1 + 0.1f * i + 0.01f * difficulty));
                    localGoods.Add(goodCopy);
                }

                ((ShopVertex)layers[i][j]).goods = localGoods;
            }
        }
    }

    private Good ChooseGood()
    {
        int choice = Random.Range(0, goodsFrequencySum);
        foreach (Good good in goods)
        {
            if (choice >= good.frequency)
            {
                choice -= good.frequency;
            }
            else return good;
        }

        return null;
    }

    private Vertex ChooseVertex(int layer)
    {
        int choice = Random.Range(0, vertexesFrequencySum);
        VertexType type = VertexType.Battle;
        foreach (var vertex in vertexesByChance)
        {
            if (choice >= vertex.Value)
            {
                choice -= vertex.Value;
            }
            else type = vertex.Key;
        }

        return type switch
        {
            VertexType.Battle => GenBattle(layer),
            VertexType.Shop => GenShop(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private bool CrossExists(List<KeyValuePair<int, int>> bounds, int c, int d)
    {
        bool res = false;
        
        foreach (var unused in bounds.Where(bound => (bound.Key - c) * (bound.Value - d) < 0))
        {
            res = true;
        }

        return res;
    }
}