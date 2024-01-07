using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Units
{
    [CreateAssetMenu(fileName = "EnemyGroup", menuName = "Enemy Group")]
    [Serializable]
    public class EnemyGroup : ScriptableObject
    {
        [SerializeField] private List<Enemy> enemiesData;
        [SerializeField] private int difficulty;
        [SerializeField] public int reward;
        [SerializeField] public bool isBoss;

        public int Difficulty => difficulty;
        public List<Enemy> GetEnemies()
        {
            return enemiesData;
        }

        public int GetReward()
        {
            return reward;
        }
    }
}