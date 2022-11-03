using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private int sizeX = 10;
    [SerializeField]
    private int sizeY = 9;
    public GridState state;
    [SerializeField]
    private Vector2 stepX;
    [SerializeField]
    private Vector2 stepY;
    [SerializeField]
    private Gem[] prefabs = new Gem[4];
    [SerializeField]
    private Vector3 chosenScale = new (1.1f, 1.1f, 1.1f);
    [SerializeField]
    private Vector3 baseScale = Vector3.one;
    [SerializeField]
    private float moveTime = 0.2f;
    [SerializeField]
    private float scaleTime = 0.2f;
    [SerializeField]
    private float refreshTime = 0.2f;
    private Gem _first;
    private Gem _second;
    private Gem[,] _box;

    private void Awake()
    {
        _box = new Gem[sizeY, sizeX];
        GenGems();
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
        for (int i = 0; i < sizeY; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {
                _box[i, j] = GenGem((Vector2)transform.position + stepX * j + stepY * i);
            }
        }
    }

    public IEnumerator OnClick(Gem gem)
    {
        switch (state)
        {
            case GridState.Choosing1:
                _first = gem;
                _first.Scale(chosenScale, scaleTime);
                yield return new WaitForSeconds(scaleTime);
                state = GridState.Choosing2;
                break;
            case GridState.Choosing2 when gem == _first:
                _first.Scale(baseScale, scaleTime);
                yield return new WaitForSeconds(scaleTime);
                _first = null;
                state = GridState.Choosing1;
                break;
            case GridState.Choosing2:
            {
                int[] pos1 = FindGem(_first);
                int[] pos2 = FindGem(gem);
                if (pos1[0] == pos2[0] && Math.Abs(pos1[1] - pos2[1]) == 1 ||
                    pos1[1] == pos2[1] && Math.Abs(pos1[0] - pos2[0]) == 1)
                {
                    _second = gem;
                    _second.Scale(chosenScale, scaleTime);
                    yield return new WaitForSeconds(scaleTime);
                    state = GridState.Moving;
                    StartCoroutine(MoveGems(_first, _second));
                    _first = null;
                    _second = null;
                }
                else
                {
                    _first.Scale(baseScale, scaleTime);
                    _first = gem;
                    _first.Scale(chosenScale, scaleTime);
                    yield return new WaitForSeconds(scaleTime);
                }
                break;
            }
        }
    }

    private IEnumerator MoveGems(Gem gem1, Gem gem2)
    {
        int[] pos1 = FindGem(gem1);
        int[] pos2 = FindGem(gem2);
        Vector2 pos = transform.position;
        gem1.Move(pos + stepX * pos2[1] + stepY * pos2[0], moveTime);
        gem2.Move(pos + stepX * pos1[1] + stepY * pos1[0], moveTime);
        yield return new WaitForSeconds(moveTime);
        _box[pos2[0], pos2[1]] = gem1;
        _box[pos1[0], pos1[1]] = gem2;
        gem1.Scale(baseScale, scaleTime);
        gem2.Scale(baseScale, scaleTime);
        yield return new WaitForSeconds(scaleTime);
        state = GridState.Refreshing;
        StartCoroutine(Refresh());
    }

    private IEnumerator Refresh()
    {
        //TODO deleting gems
        yield return new WaitForSeconds(refreshTime);
        //TODO generating gems
        state = GridState.Choosing1;
    }

    private int[] FindGem(Gem gem)
    {
        int[] res = { 0, 0 };
        for (int i = 0; i < sizeY; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {
                if (_box[i, j] == gem)
                {
                    res = new[] { i, j };
                }
            }
        }
        return res;
    }
}