using System;
using System.Collections.Generic;
using Battle;
using Battle.Match3;
using Battle.Units;
using JetBrains.Annotations;
using UnityEngine;

namespace Battle
{
    public class BattleBeginLog : Log
    {
        public static void Log() => AddLog(new BattleBeginLog());
    }
    
    [Serializable]
    public class TurnLog : Log
    {
        public static void Log() => AddLog(new TurnLog());
    }
    
    [Serializable]
    public class GridLog : Log
    {
        private Dictionary<GemType, int> _table;
    
        public static void Log(Dictionary<GemType, int> table) => AddLog(new GridLog(table));

        public Dictionary<GemType, int> Data() => _table;

        private GridLog(Dictionary<GemType, int> table) => _table = table;
    }

    [Serializable]
    public class DeathLog : Log
    {
        [SerializeField] private Unit unit;
    
        public static void Log(Unit unit) => AddLog(new DeathLog(unit));

        public Unit Data() => unit;

        private DeathLog(Unit unit) => this.unit = unit;
    }

    [Serializable]
    public class DamageLog : Log
    {
        [SerializeField] [CanBeNull] private Unit applicator;
        [SerializeField] [CanBeNull] private Unit target;
        [SerializeField] private int damage;

        public static void Log([CanBeNull] Unit applicator, [CanBeNull] Unit target, int damage) =>
            AddLog(new DamageLog(applicator, target, damage));

        public (Unit, Unit, int) Data() => (applicator, target, damage);

        private DamageLog(Unit applicator, Unit target, int damage)
        {
            this.applicator = applicator;
            this.target = target;
            this.damage = damage;
        }
    }

    public class Log
    {
        public static AddedLog logger;
        protected static void AddLog(Log log)
        {
            BattleManager.Logs.Add(log);
            logger?.Invoke(log);
        }
    }
}
public delegate void AddedLog(Log log);