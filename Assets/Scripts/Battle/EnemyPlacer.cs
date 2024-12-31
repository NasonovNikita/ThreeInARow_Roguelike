using System.Collections.Generic;
using Battle.Units;
using UnityEngine;

namespace Battle
{
    /// <summary>
    ///     Visually places enemies to given anchor points.2
    /// </summary>
    public class EnemyPlacer : MonoBehaviour
    {
        [SerializeField] private GameObject[] points;
        public static EnemyPlacer Instance { get; private set; }

        public void Awake()
        {
            Instance = this;
        }

        public void Place(List<Enemy> enemiesToPlace)
        {
            for (var i = 0; i < enemiesToPlace.Count; i++)
            {
                var enemy = enemiesToPlace[i];
                if (enemy == null) continue;
                enemy.transform.position = points[i].transform.position;
            }
        }
    }
}