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
        [FormerlySerializedAs("enemiesData")] [SerializeField]
        private List<Enemy> enemies;

        [SerializeField] private int difficulty;
        [SerializeField] private int reward;
        [SerializeField] public bool isBoss;

        public int Difficulty => difficulty;
        public List<Enemy> Enemies => enemies;
        public int Reward => reward;
    }
}