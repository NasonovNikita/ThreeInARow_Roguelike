using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
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
        [VertexType.Battle] = 13,
        [VertexType.Shop] = 4
    };
    private int vertexesFrequencySum;

    private Good[] goods;
    private int goodsFrequencySum;
    
    public KeyValuePair<List<List<VertexData>>, List<List<KeyValuePair<int, int>>>> GetMap(int seed)
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

        var layers =  Generate();
        
        
        var bounds = BindLayers(layers);

        
        Random.InitState((int) DateTime.Now.Ticks);
        return new KeyValuePair<List<List<VertexData>>, List<List<KeyValuePair<int, int>>>>(layers, bounds);
    }

    private List<List<VertexData>> Generate()
    {
        List<List<VertexData>> layers = new() { new List<VertexData> {GenBattle(0)} };

        for (int i = 1; i < depth - 1; i++)
        {
            layers.Add(GenLayer(i));
        }

        layers.Add(new List<VertexData> {GenBattle(depth)});
        
        PlaceVertexes(layers);

        return layers;
    }

    private void PlaceVertexes(IReadOnlyList<List<VertexData>> layers)
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

    private void PlaceLayer(IReadOnlyList<VertexData> layer, int i, Vector3 xStep, Vector3 yStep)
    {
        int k = layer.Count;
        layer[0].position = down.transform.position + i * yStep + ((float) k - 1) / 2 * -xStep;
        for (int j = 1; j < k; j++)
        {
            layer[j].position = layer[j - 1].position + xStep + Random.Range(-25, 26) * dX + Random.Range(-25, 26) * dY;
        }
    }

    private List<VertexData> GenLayer(int layer)
    {
        List<VertexData> resultLayer = new();

        int width = Random.Range(minWidth, maxWidth + 1);

        for (int i = 0; i < width; i++)
        {
            resultLayer.Add(ChooseVertex(layer));
        }

        return resultLayer;
    }

    private List<List<KeyValuePair<int, int>>> BindLayers(IReadOnlyList<List<VertexData>> layers)
    {
        List<List<KeyValuePair<int, int>>> bounds = new();
        for (int i = 0; i < depth - 1; i++)
        {
            bounds.Add(Bind2Layers(layers[i], layers[i + 1]));
        }

        return bounds;
    }

    private static List<KeyValuePair<int, int>> Bind2Layers(ICollection oldLayer, ICollection newLayer)
    {
        HashSet<int> boundVertexes = new();

        List<KeyValuePair<int, int>> bounds = new();

        while (boundVertexes.Count != oldLayer.Count + newLayer.Count)
        {
            int a = Random.Range(0, oldLayer.Count);
            int b = Random.Range(0, newLayer.Count);
            
            if (CrossExists(bounds, a, b) || bounds.Exists(pair => pair.Key == a && pair.Value == b)) continue;
            bounds.Add(new KeyValuePair<int, int>(a, b));
            boundVertexes.AddRange(new[] { a, -b - 1 });
        }

        return bounds;
    }

    private BattleVertexData GenBattle(int layer)
    {
        BattleVertexData vertex = new();

        int battleDifficulty = difficulty + layer * difficulty / 3 + Random.Range(-5, 6);
        int chosenKey = groups.Keys.Aggregate(
                (min, next) => Math.Abs(min - battleDifficulty) < Math.Abs(next - battleDifficulty) ? min : next
                );

        
        EnemyGroup group = Instantiate(groups[chosenKey][Random.Range(0, groups[chosenKey].Count)]);
        vertex.group = group;
        
        return vertex;
    }

    private ShopVertexData GenShop(int layer)
    {
        ShopVertexData vertex = new();

        List<Good> currentGoods = new();

        for (int i = 0; i < 4; i++)
        {
            currentGoods.Add(ChooseGood(layer));
        }

        vertex.goods = currentGoods;

        return vertex;
    }

    private Good ChooseGood(int layer)
    {
        int choice = Random.Range(0, goodsFrequencySum);
        foreach (Good good in goods)
        {
            if (choice >= good.frequency)
            {
                choice -= good.frequency;
            }
            else
            {
                Good goodCopy = Instantiate(good);
                goodCopy.price = (int)(goodCopy.price * (1 + 0.1f * layer + 0.01f * difficulty));
                return goodCopy;
            }
        }

        return null;
    }

    private VertexData ChooseVertex(int layer)
    {
        int choice = Random.Range(0, vertexesFrequencySum);
        VertexType type = VertexType.Battle;
        foreach (var vertex in vertexesByChance)
        {
            if (choice >= vertex.Value)
            {
                choice -= vertex.Value;
            }
            else
            {
                type = vertex.Key;
                break;
            }
        }

        return type switch
        {
            VertexType.Battle => GenBattle(layer),
            VertexType.Shop => GenShop(layer),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static bool CrossExists(IEnumerable<KeyValuePair<int, int>> bounds, int c, int d)
    {
        bool res = false;
        
        foreach (var unused in bounds.Where(bound => (bound.Key - c) * (bound.Value - d) < 0))
        {
            res = true;
        }

        return res;
    }
}