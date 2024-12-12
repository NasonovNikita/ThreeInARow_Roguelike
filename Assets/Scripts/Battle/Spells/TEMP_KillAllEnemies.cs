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
            foreach (var enemy in BattleFlowManager.Instance.EnemiesAlive)
                enemy.TakeDamage(100000);
        }
    }
}