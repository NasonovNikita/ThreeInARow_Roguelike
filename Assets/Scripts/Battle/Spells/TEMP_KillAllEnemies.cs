using Battle.Units;
using UnityEngine;

namespace Battle.Spells
{
    // ReSharper disable once InconsistentNaming
    [CreateAssetMenu(menuName = "Spells/KillAllEnemies", fileName = "TEMP_KillAllEnemies",
        order = 0)]
    public class TEMP_KillAllEnemies : Spell
    {
        protected override void Action()
        {
            foreach (Enemy enemy in BattleFlowManager.Instance.EnemiesWithoutNulls)
                enemy.TakeDamage(100000);
        }
    }
}