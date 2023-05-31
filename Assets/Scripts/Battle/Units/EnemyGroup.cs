using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyGroup", menuName = "Enemy Group")]
public class EnemyGroup : ScriptableObject
{
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private int difficulty;

    public List<Enemy> GetEnemies()
    {
        return enemies;
    }

    public int Difficulty()
    {
        return difficulty;
    }
}
