using UnityEngine;

namespace Core
{
    public class Globals : MonoBehaviour
    {
        public static Globals instance;

        public bool randomSeed;

        public int seed;

        public float volume;

        public float difficulty; // never make int

        public bool altBattleUI;

        public (int, int) gridSize;
    
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
}