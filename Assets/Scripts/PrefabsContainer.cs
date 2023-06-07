using Battle.Units.Enemies;
using UnityEngine;

public class PrefabsContainer : MonoBehaviour
{
    public Enemy enemy;

    public GameObject winMessage;

    public GameObject loseMessage;

    public static PrefabsContainer instance;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }
}
