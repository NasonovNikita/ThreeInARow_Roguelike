using UnityEngine;

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

    private void Awake()
    {
        state = GridState.refreshing;
        transform.position = position;
        GenGems();
    }

    private Gem GenGem(Vector2 gemPosition) {
        Gem gem = Instantiate(prefabs[Random.Range(0, prefabs.Length)]);
        gem.transform.position = gemPosition;
        gem.grid = this;
        return gem;
    }

    private void GenGems() {
        Gem[,] box = new Gem[sizeY, sizeX];
        for (int i = 0; i < sizeY; i++) {
            for (int j = 0; j < sizeX; j++) {
                box[i, j] = GenGem((Vector2)transform.position + stepX * j + stepY * i);
            }
        }
    }
}