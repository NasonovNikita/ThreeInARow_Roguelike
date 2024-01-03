using System.Collections.Generic;
using UI;
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
                if (enemy == null) continue;
                enemy.transform.position = points[i].transform.position;
            }
        }
    }
}