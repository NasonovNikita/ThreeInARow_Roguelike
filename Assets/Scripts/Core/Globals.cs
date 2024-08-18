using UnityEngine;

namespace Core
{
    /// <summary>
    ///     Global data (settings, game values, etc.).
    /// </summary>
    public class Globals : MonoBehaviour
    {
        public bool randomSeed;

        public int seed;

        public float volume;

        public float difficulty; // never make int

        public bool altBattleUI;

        public float timeBetweenSymbolsInPlotDialog;

        public (int, int) GridSize;
        public static Globals Instance { get; private set; }

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}