#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using Battle;
using Battle.Modifiers;
using Battle.Units;
using Battle.Units.Stats;

namespace DevTools.DebugLogging.LoggingGroups.Battle
{
    public class UnitLoggingGroup : DebugLoggingGroup
    {
        public override void Attach()
        {
            foreach (Unit unit in new List<Unit>(BattleFlowManager.Instance.EnemiesWithoutNulls)
                         { Player.Instance })
            {
                unit.OnDied += () => CheckAndWrite($"{UnitInfo(unit)} died");
                unit.OnMadeHit += () => CheckAndWrite($"{UnitInfo(unit)} made hit");
                unit.OnSpellCasted += () => CheckAndWrite($"{UnitInfo(unit)} casted spell");
                AttachToStatChanges(unit);
                AttachToModLists(unit);
            }

            return;

            string UnitInfo(Unit unit)
            {
                return unit switch
                {
                    Player => "Player",
                    Enemy enemy => EnemyInfo(enemy),
                    _ => throw new ArgumentOutOfRangeException(nameof(unit), unit, null)
                };
            }

            string EnemyInfo(Enemy enemy)
            {
                return $"Enemy ({enemy.name}) " +
                       $"at index {BattleFlowManager.Instance.EnemiesWithoutNulls.IndexOf(enemy)}";
            }

            void AttachToStatChanges(Unit unit)
            {
                foreach (Stat stat in GetStats())
                    stat.OnValueChanged += value => WriteStatChange(stat, value);

                return;

                void WriteStatChange(Stat stat, int value)
                {
                    CheckAndWrite($"{unit.name}' {stat} changed by {value}");
                }

                List<Stat> GetStats()
                {
                    return new List<Stat> { unit.hp, unit.mana, unit.damage };
                }
            }

            void AttachToModLists(Unit unit)
            {
                foreach (ModifierList list in unit.AllModifierLists)
                    list.OnModAdded += modifier =>
                        CheckAndWrite($"{unit.name} got {modifier} to {list}");
            }
        }

        public override void UnAttach()
        {
            // ignored
        }
    }
}

#endif