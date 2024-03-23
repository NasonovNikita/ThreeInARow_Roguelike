using System.Collections.Generic;
using UnityEngine;

namespace Battle.Units
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