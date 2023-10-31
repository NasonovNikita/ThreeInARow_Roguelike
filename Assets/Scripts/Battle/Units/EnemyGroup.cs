using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyGroup", menuName = "Enemy Group")]
[Serializable]
public class EnemyGroup : ScriptableObject
{
    [SerializeField] private List<Enemy> enemiesData;
    [SerializeField] private int difficulty;
    [SerializeField] public int reward;

    public List<Enemy> GetEnemies()
    {
        return enemiesData;
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