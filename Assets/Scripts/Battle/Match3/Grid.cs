using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private int sizeX;
    [SerializeField]
    private int sizeY;
    
    [SerializeField]
    private Vector2 stepX;
    [SerializeField]
    private Vector2 stepY;
    
    [SerializeField]
    private Gem[] prefabs;

    [SerializeField]
    private float moveTime;
    [SerializeField]
    private float scaleTime;
    [SerializeField]
    private float refreshTime;

    public readonly Dictionary<GemType, int> Destroyed = new();

    public BattleManager manager;
    
    private GridState _state;

    private Gem _first;
    private Gem _second;
    
    private Gem[,] _box;

    private void Awake()
    {
        BattleManager.Grid = this;
        if (BattleManager.Player != null)
        {
            BattleManager.TurnOn();
        }
        _box = new Gem[sizeY, sizeX];
        SmartGenGems();
    }

    private Gem GenGem(int i, int j, int type = 0)
    {
        Gem gem = prefabs[type != 0 ? type : Random.Range(0, prefabs.Length)];
        gem.transform.position = (Vector2)transform.position + stepX * j + stepY * i;
        gem.grid = this;
        return gem;
    }

    public void Block()
    {
        _state = GridState.Blocked;
    }
    public void Unlock()
    {
        _state = GridState.Choosing1;
    }

    public IEnumerator OnClick(Gem gem)
    {
        switch (_state)
        {
            case GridState.Choosing1:
                
                _first = gem;
                
                _first.Scale(gem.ChosenScale, scaleTime);
                
                _state = GridState.Choosing2;
                
                yield return new WaitForSeconds(scaleTime);
                break;
            
            case GridState.Choosing2 when gem == _first:
                
                _first.Scale(gem.BaseScale, scaleTime);
                
                _first = null;
                
                _state = GridState.Choosing1;
                
                yield return new WaitForSeconds(scaleTime);
                break;
            
            case GridState.Choosing2:
            {
                _second = gem;
                
                
                if (GemsArenNeighbours(_first, _second))
                {
                    
                    _second.Scale(gem.ChosenScale, scaleTime);
                    yield return new WaitForSeconds(scaleTime);
                    
                    _state = GridState.Moving;
                    StartCoroutine(MoveGems(_first, _second));
                    
                    _first = null;
                    _second = null;
                }
                else
                {
                    _first.Scale(gem.BaseScale, scaleTime);
                    _second.Scale(gem.ChosenScale, scaleTime);
                    
                    _first = _second;
                    _second = null;
                    
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
        
        gem1.Move(gem2.transform.position, moveTime);
        gem2.Move(gem1.transform.position, moveTime);
        yield return new WaitForSeconds(moveTime);
        
        _box[pos2[0], pos2[1]] = gem1;
        _box[pos1[0], pos1[1]] = gem2;
        
        gem1.Scale(gem1.BaseScale, scaleTime);
        gem2.Scale(gem2.BaseScale, scaleTime);
        yield return new WaitForSeconds(scaleTime);
        
        _state = GridState.Refreshing;
        StartCoroutine(Refresh());
    }

    private IEnumerator Refresh()
    {
        HashSet<Gem> toDelete = new HashSet<Gem>();
        
        for (int i = 0; i < sizeY; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {
                if (HorizontalRowExists(i, j))
                {
                    toDelete.Add(_box[i, j]);
                    toDelete.Add(_box[i, j + 1]);
                    toDelete.Add(_box[i, j + 2]);
                }
                if (VerticalRowExists(i, j))
                {
                    toDelete.Add(_box[i, j]);
                    toDelete.Add(_box[i + 1, j]);
                    toDelete.Add(_box[i + 2, j]);
                }
            }
        }

        if (toDelete.Count == 0)
        {
            Block();
            manager.EndTurn();
            yield break;
        }
        
        Destroyed.Clear();
        foreach (Gem gem in toDelete)
        {
            if (Destroyed.TryGetValue(gem.Type, out int val))
            {
                Destroyed[gem.Type] = val + 1;
            }
            else
            {
                Destroyed.Add(gem.Type, 1);
            }
        }

        yield return new WaitForSeconds(refreshTime);
        foreach (Gem gem in toDelete)
        {
            Destroy(gem.gameObject);
        }

        
        yield return new WaitForSeconds(refreshTime);
        for (int i = 0; i < sizeY; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {
                if (_box[i, j] == null)
                {
                    _box[i, j] = Instantiate(GenGem(i, j));
                }
            }
        }
        
        StartCoroutine(Refresh());
    }

    private void SmartGenGems()
    {
        for (int i = sizeY - 1; i >= 0; i--)
        {
            for (int j = sizeX - 1; j >= 0; j--)
            {
                int type = 0;
                do
                {
                    _box[i, j] = GenGem(i, j, type);
                    type++;
                } while (HorizontalRowExists(i, j) || VerticalRowExists(i, j));

                _box[i, j] = Instantiate(_box[i, j]);
            }
        }
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

    private bool GemsArenNeighbours(Gem gem1, Gem  gem2)
    {
        int[] pos1 = FindGem(gem1);
        int[] pos2 = FindGem(gem2);
        
        return pos1[0] == pos2[0] && Math.Abs(pos1[1] - pos2[1]) == 1 ||
               pos1[1] == pos2[1] && Math.Abs(pos1[0] - pos2[0]) == 1;
    }

    private bool HorizontalRowExists(int i, int j)
    {
        return j < sizeX - 2 && _box[i, j].Type == _box[i, j + 1].Type && _box[i, j].Type == _box[i, j + 2].Type;
    }

    private bool VerticalRowExists(int i, int j)
    {
        return i < sizeY - 2 && _box[i, j].Type == _box[i + 1, j].Type && _box[i, j].Type == _box[i + 2, j].Type;
    }
}