using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private int sizeX;
    [SerializeField]
    private int sizeY;
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
    private Vector3 chosenScale;
    private readonly Vector3 _baseScale = Vector3.one;
    private Gem _first;
    private Gem _second;
    

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
        Gem[,] box = new Gem[sizeY, sizeX];
        for (int i = 0; i < sizeY; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {
                box[i, j] = GenGem((Vector2)transform.position + stepX * j + stepY * i);
            }
        }
    }

    public void OnClick(Gem gem)
    {
        switch (state)
        {
            case GridState.choosing1:
                _first = gem;
                gem.Scale(chosenScale);
                state = GridState.choosing2;
                break;
            case GridState.choosing2:
            {
                if (gem == _first)
                {
                    _first = null;
                    gem.Scale(_baseScale);
                    state = GridState.choosing1;
                }
                else
                {
                    _second = gem;
                    gem.Scale(chosenScale);
                    state = GridState.moving;
                }
                break;
            }
        }
    }

    private IEnumerator MoveGems(Gem gem1, Gem gem2)
    {
        //moving...
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
}