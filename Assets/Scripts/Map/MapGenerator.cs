using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    public int seed;

    public int difficulty;
    public int depth;
    public int maxWidth;

    private List<List<Vertex>> layers = new ();

    public void Awake()
    {
        Random.InitState(seed);
    }

    private void GenBattle()
    {
        
    }

    private void GenShop()
    {
        
    }
}