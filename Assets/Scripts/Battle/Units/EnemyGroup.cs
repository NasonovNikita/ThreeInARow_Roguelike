using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Battle.Units
{
    [CreateAssetMenu(fileName = "EnemyGroup", menuName = "Enemy Group")]
    [Serializable]
    public class EnemyGroup : ScriptableObject
    {
        [FormerlySerializedAs("enemiesData")] [SerializeField] private List<Enemy> enemies;
        [SerializeField] private int difficulty;
        [SerializeField] public int reward;
        [SerializeField] public bool isBoss;

        public int Difficulty => difficulty;
        public List<Enemy> GetEnemies()
        {
            return enemies;
        }

        public int GetReward()
        {
            return reward;
        }
    }
}