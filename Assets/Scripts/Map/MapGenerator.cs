using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private BattleVertex battlePrefab;
    [SerializeField] private ShopVertex shopPrefab;
    
    public int seed;

    public int difficulty;
    public int depth;
    public int minWidth;
    public int maxWidth;

    private readonly Dictionary<int, List<EnemyGroup>> groups = new();

    private readonly Dictionary<VertexType, int> vertexesByChance = new ()
    {
        [VertexType.Battle] = 10,
        [VertexType.Shop] = 3
    };
    private int vertexesFrequencySum;

    private Good[] goods;
    private int goodsFrequencySum;
    
    public void Awake()
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

        var layers = Generate();

        Map.vertexesPrefabs = layers;
    }

    private List<List<Vertex>> Generate()
    {
        List<List<Vertex>> layers = new();

        BattleVertex first = GenBattle(0);
        layers.Add(new List<Vertex> {first});
        
        for (int i = 1; i < depth - 1; i++)
        {
            layers.Add(GenLayer(i));
        }

        BattleVertex last = GenBattle(depth - 1);
        layers.Add(new List<Vertex> {last});

        for (int i = 0; i < depth - 1; i++)
        {
            BindLayers(layers[i], layers[i + 1]);
        }
        
        return layers;
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

    private void BindLayers(List<Vertex> oldLayer, List<Vertex> newLayer)
    {
        
    }

    private BattleVertex GenBattle(int layer)
    {
        BattleVertex vertex = battlePrefab;

        int battleDifficulty = difficulty + layer * difficulty + Random.Range(-10, 10 + 1);
        int chosenKey = groups.Keys.Aggregate(
                (min, next) => Math.Abs(min - battleDifficulty) < Math.Abs(next - battleDifficulty) ? min : next
                );

        
        EnemyGroup group = groups[chosenKey][Random.Range(0, groups[chosenKey].Count)];
        vertex.enemies = group.GetEnemies();
        
        return vertex;
    }

    private ShopVertex GenShop(int layer)
    {
        ShopVertex vertex = shopPrefab;

        List<Good> currentGoods = new();

        for (int i = 0; i < 4; i++)
        {
            var good = ChooseGood();
            var price = good.price;
            
            price = (int) (price * (1 + 0.1f * layer + 0.01f * difficulty));
            good.price = price;
            
            currentGoods.Add(good);
        }

        vertex.goods = currentGoods;

        return vertex;
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
            VertexType.Shop => GenShop(layer),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}