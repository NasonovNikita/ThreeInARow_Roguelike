using System.Collections.Generic;
using UnityEngine;

namespace Battle.Units
{
    public class EnemyPlacer : MonoBehaviour
    {
        public GameObject[] points;
    
        public List<Enemy> enemiesToPlace;
   
    
        public void Place()
        {
            for (int i = 0; i < enemiesToPlace.Count; i++)
            {
                Enemy enemy = enemiesToPlace[i];
                if (enemy == null) return;
                enemy.transform.position = points[i].transform.position;
            }
        }
    }
}