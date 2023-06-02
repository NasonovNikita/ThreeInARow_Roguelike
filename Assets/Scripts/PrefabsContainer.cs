using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsContainer : MonoBehaviour
{
    public Enemy enemy;

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
