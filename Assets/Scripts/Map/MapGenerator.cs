using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    public int seed;

    public int difficulty;
    public int depth;
    public int minWidth;
    public int maxWidth;

    private readonly Dictionary<int, List<EnemyGroup>> groups = new();

    private Map map;

    public void Awake()
    {
        Random.InitState(seed);
        
        map = FindFirstObjectByType<Map>();
        foreach (EnemyGroup group in Resources.LoadAll<EnemyGroup>("Presets/EnemyGroups"))
        {
            if (!groups.TryAdd(group.Difficulty(), new List<EnemyGroup> {group}))
            {
                groups[group.Difficulty()].Add(group);
            }
        }

        List<List<Vertex>> layers = Generate();
    }

    private List<List<Vertex>> Generate()
    {
        /*
         * Start with single battle vertex
         *
         * minWidth - maxWidth vertexes in a layer, more difficulty - less vertexes in a layer
         * Choosing of vertex type depends on difficulty (higher difficulty - more battles, for example)
         *
         * Ends with single battle vertex (some kind of boss?)
         */

        List<List<Vertex>> layers = new();

        BattleVertex first = GenBattle(0);
        layers.Add(new List<Vertex> {first});
        
        for (int i = 0; i < depth - 2; i++)
        {
            
        }

        BattleVertex last = GenBattle(depth);
        layers.Add(new List<Vertex> {last});
        

        return layers;
    }

    private BattleVertex GenBattle(int layer)
    {
        BattleVertex vertex = new BattleVertex();

        int battleDifficulty = difficulty + layer * difficulty + Random.Range(-10, 10 + 1);
        int chosenKey = groups.Keys.Aggregate(
                (min, next) => Math.Abs(min - battleDifficulty) < Math.Abs(next - battleDifficulty) ? min : next
                );

        
        EnemyGroup group = groups[chosenKey][Random.Range(0, groups[chosenKey].Count)];
        vertex.enemies = group.GetEnemies();
        
        return vertex;
    }

    private ShopVertex GenShop()
    {
        /*
         * 1-4 goods
         *
         * Goods in the shop are chosen by rarity
         * Rare goods obviously appear rarely, NOT affected bu difficulty
         * Price depends on main preset price for good + layer margin + difficulty
         */

        ShopVertex vertex = new ShopVertex();

        return vertex;
    }

    private void BindLayers(List<Vertex> oldLayer, List<Vertex> newLayer)
    {
        /*
         * Every vertex from oldLayer must have at least one vertex to bound to
         * Every vertex from newLayer must have at least one vertex that bound to it
         *
         * Binding depends on random * difficulty (lower difficulty - more chance and vice versa)
         * Edges mustn't cross each other
         */
    }
}