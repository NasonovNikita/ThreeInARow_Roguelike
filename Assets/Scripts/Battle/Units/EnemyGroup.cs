using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Units.Data;
using Battle.Units.Enemies;
using UnityEngine;

namespace Battle.Units
{
    [CreateAssetMenu(fileName = "EnemyGroup", menuName = "Enemy Group")]
    [Serializable]
    public class EnemyGroup : ScriptableObject
    {
        [SerializeField] private List<EnemyData> enemiesData;
        [SerializeField] private int difficulty;
        [SerializeField] public int reward;

        public List<Enemy> GetEnemies()
        {
            return enemiesData.Select(data => data.Init()).ToList();
        }

        public int GetReward()
        {
            return reward;
        }

        public int Difficulty()
        {
            return difficulty;
        }
    }
}