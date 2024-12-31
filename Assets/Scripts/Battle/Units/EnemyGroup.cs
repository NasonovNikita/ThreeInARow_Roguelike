using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Battle.Units
{
    /// <summary>
    ///     A holder of battle data
    ///     (<see cref="Enemies"/>, <see cref="Reward"/>, <see cref="Difficulty"/>, etc.).
    /// </summary>
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
        public List<Enemy> Enemies => new(enemies);
        public int Reward => reward;
    }
}