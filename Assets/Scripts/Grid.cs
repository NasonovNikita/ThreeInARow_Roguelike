using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private int SizeX;
    [SerializeField]
    private int SizeY;
    [SerializeField]
    private Vector2 position;
    public GridState State;
    [SerializeField]
    private Vector2 StepX;
    [SerializeField]
    private Vector2 StepY;
    [SerializeField]
    private GameObject[] prefabs = new GameObject[4];

    private void Awake()
    {
        State = GridState.refreshing;
        transform.position = position;
        GenGems();
    }

    private GameObject GenGem(Vector2 position) {
        GameObject gem = Instantiate(prefabs[Random.Range(0, prefabs.Length)]);
        gem.transform.position = position;
        return gem;
    }

    public void GenGems() {
        GameObject[,] box = new GameObject[SizeY, SizeX];
        for (int i = 0; i < SizeY; i++) {
            for (int j = 0; j < SizeX; j++) {
                box[i, j] = GenGem((Vector2)transform.position + StepX * j + StepY * i);
            }
        }
    }
}