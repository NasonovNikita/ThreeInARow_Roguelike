using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Grid : MonoBehaviour
{
    private static readonly int SizeX = 10;
    private static readonly int SizeY = 8;
    [SerializeField]
    private Vector2 position;
    public GridState state;
    [SerializeField]
    private Vector2 stepX;
    [SerializeField]
    private Vector2 stepY;
    [SerializeField]
    private Gem[] prefabs = new Gem[4];
    [SerializeField]
    private Vector3 _chosenScale;
    private readonly Vector3 _baseScale = Vector3.one;
    private Gem _first;
    private Gem _second;
    private Gem[,] _box = new Gem[SizeY, SizeX];
    

    private void Awake()
    {
        state = GridState.choosing1;
        transform.position = position;
        GenGems();
    }

    private void Update()
    {
        if (state == GridState.moving)
        {
            StartCoroutine(MoveGems(_first, _second));
        }
        else if (state == GridState.refreshing)
        {
            Refresh();
        }
    }

    private Gem GenGem(Vector2 gemPosition)
    {
        Gem gem = Instantiate(prefabs[Random.Range(0, prefabs.Length)]);
        gem.transform.position = gemPosition;
        gem.grid = this;
        return gem;
    }

    private void GenGems()
    {
        for (int i = 0; i < SizeY; i++)
        {
            for (int j = 0; j < SizeX; j++)
            {
                _box[i, j] = GenGem((Vector2)transform.position + stepX * j + stepY * i);
            }
        }
    }

    public void OnClick(Gem gem)
    {
        switch (state)
        {
            case GridState.choosing1:
                _first = gem;
                _first.Scale(_chosenScale);
                state = GridState.choosing2;
                break;
            case GridState.choosing2:
            {
                if (gem == _first)
                {
                    _first.Scale(_baseScale);
                    _first = null;
                    state = GridState.choosing1;
                }
                else
                {
                    int[] pos1 = FindGem(_first);
                    int[] pos2 = FindGem(gem);
                    if (pos1[0] == pos2[0] & Math.Abs(pos1[1] - pos2[1]) == 1 | pos1[1] == pos2[1] & Math.Abs(pos1[0] - pos2[0]) == 1)
                    {
                        _second = gem;
                        _second.Scale(_chosenScale);
                        state = GridState.moving;
                    }
                    else
                    {
                        _first.Scale(_baseScale);
                        _first = gem;
                        _first.Scale(_chosenScale);
                    }
                }
                break;
            }
        }
    }

    private IEnumerator MoveGems(Gem gem1, Gem gem2)
    {
        yield return new WaitForSeconds(0.5f);
        gem1.Scale(_baseScale);
        gem2.Scale(_baseScale);
        state = GridState.refreshing;
    }

    private void Refresh()
    {
        //refreshing...
        state = GridState.choosing1;
    }

    private int[] FindGem(Gem gem)
    {
        int a = 0;
        int b = 0;
        for (int i = 0; i < SizeY; i++)
        {
            for (int j = 0; j < SizeX; j++)
            {
                if (_box[i, j] == gem)
                {
                    a = i;
                    b = j;
                }
            }
        }
        
        return new [] { a, b };
    }
}