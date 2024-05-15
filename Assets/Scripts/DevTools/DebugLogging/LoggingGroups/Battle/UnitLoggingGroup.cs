using System.Collections.Generic;
using Battle;
using Battle.Units;
using Battle.Units.Stats;

namespace DevTools.DebugLogging.LoggingGroups.Battle
{
    public class UnitLoggingGroup : DebugLoggingGroup
    {
        public override void Attach()
        {

            Player.Instance.OnDied += () => CheckAndWrite("Player died");
            Player.Instance.OnMadeHit += () => CheckAndWrite("Player made hit");
            Player.Instance.OnSpellCasted += () => CheckAndWrite("Player casted spell");
            AttachToStatChanges(Player.Instance);
             

            foreach (Enemy enemy in BattleFlowManager.Instance.EnemiesWithoutNulls)
            {
                enemy.OnDied += () => CheckAndWrite($"{ EnemyInfo() } died");
                enemy.OnMadeHit += () => CheckAndWrite($"{ EnemyInfo() } made hit");
                enemy.OnSpellCasted += () => CheckAndWrite($"{ EnemyInfo() } casted spell");
                AttachToStatChanges(enemy);

                continue;
                
                string EnemyInfo() =>
                    $"Enemy ({enemy.name}) " +
                    $"at index {BattleFlowManager.Instance.EnemiesWithoutNulls.IndexOf(enemy)}";
            }
            
            return;

            void AttachToStatChanges(Unit unit)
            {
                foreach (Stat stat in GetStats()) 
                    stat.OnValueChanged += value => WriteStatChange(stat, value);
                
                return;
                
                void WriteStatChange(Stat stat, int value) => 
                    CheckAndWrite($"Unit's ({unit.name}) stat ({stat}) changed by {value}");

                List<Stat> GetStats() => new() { unit.hp, unit.mana, unit.damage };
            }
        }

        public override void UnAttach()
        {
            // ignored
        }
    }
}