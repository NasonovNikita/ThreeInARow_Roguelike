using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    public int seed;

    public int difficulty;
    public int depth;
    public int minWidth;
    public int maxWidth;

    private List<List<Vertex>> layers;

    private Map map;

    public void Awake()
    {
        Random.InitState(seed);
        map = FindFirstObjectByType<Map>();
    }

    private void Generate()
    {
        /*
         * Start with single battle vertex
         *
         * minWidth - maxWidth vertexes in a layer, more difficulty - less vertexes in a layer
         * Choosing of vertex type depends on difficulty (higher difficulty - more battles, for example)
         *
         * Ends with single battle vertex (some kind of boss?)
         */
    }

    private void GenBattle()
    {
        /*
         * 1-5 enemies per room
         *
         * I think it's better to use preset rooms to make it more balanced, otherwise it can be too random
         * Chosen by weight (difficulty) + random in allowed range
         * Difficulty of a room is calculated using whole game difficulty + layer + small random
         */
    }

    private void GenShop()
    {
        /*
         * 1-4 goods
         *
         * Goods in the shop are chosen by rarity
         * Rare goods obviously appear rarely, NOT affected bu difficulty
         * Price depends on main preset price for good + layer margin + difficulty
         */
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