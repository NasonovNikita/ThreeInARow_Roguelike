using System.Collections.Generic;
using Battle.Units;
using UnityEngine;

namespace Battle
{
    public class EnemyPlacer : MonoBehaviour
    {
        [SerializeField] private GameObject[] points;
        
        public void Place(List<Enemy> enemiesToPlace)
        {
            for (int i = 0; i < enemiesToPlace.Count; i++)
            {
                Enemy enemy = enemiesToPlace[i];
                if (enemy == null) continue;
                enemy.transform.position = points[i].transform.position;
            }
        }
    }
}