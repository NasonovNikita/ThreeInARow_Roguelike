using UnityEngine;

namespace Core
{
    public class Globals : MonoBehaviour
    {
        public static Globals Instance { get; private set; }

        public bool randomSeed;

        public int seed;

        public float volume;

        public float difficulty; // never make int

        public bool altBattleUI;

        public (int, int) gridSize;
    
        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }
    }
}