using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Match3;
using Battle.Units;

namespace Battle
{
    public static class BattleLog
    {
        public static List<T> GetLogsOfType<T>() => AllLogs.Where(log => log is T).Cast<T>().ToList();

        public static List<Log> AllLogs { get; } = new();

        public static Log LastLog => AllLogs[^1];

        public static List<Log> GetLastTurn()
        {
            if (!AllLogs.Exists(v => v is TurnLog) || AllLogs.Count(val => val is TurnLog) == 1)
            {
                return AllLogs;
            }

            int i = Math.Max(AllLogs.Count - 2, 0);
            while (i >= 0)
            {
                if (AllLogs[i] is TurnLog) break;
                i--;
            }

            return AllLogs.ToArray()[i..^1].ToList();
        }

        public static void Clear() => AllLogs.Clear();

        internal static void AddLog(Log log) => AllLogs.Add(log);
    }

    public class GridLog : Log
    {
        private readonly Dictionary<GemType, int> table;
    
        public static void Log(Dictionary<GemType, int> table) => AddLog(new GridLog(table));

        public Dictionary<GemType, int> GetData => table;

        private GridLog(Dictionary<GemType, int> table) => this.table = table;
    }

    public class SpellUsageLog : Log
    {
        private readonly Unit unit;

        private readonly int wasted;

        public (Unit, int) GetData => (unit, wasted);
        
        public static void Log(Unit unit, int wasted) => AddLog(new SpellUsageLog(unit, wasted));

        private SpellUsageLog(Unit unit, int wasted)
        {
            this.unit = unit;
            this.wasted = wasted;
        }
    }
    
    public class TurnLog : Log
    {
        public static void Log() => AddLog(new TurnLog());
    }

    public class BattleEndLog : Log
    {
        public static void Log() => AddLog(new BattleEndLog());
    }

    public class DeathLog : Log
    {
        private readonly Unit unit;
    
        public static void Log(Unit unit) => AddLog(new DeathLog(unit));

        public Unit GetData => unit;

        private DeathLog(Unit unit) => this.unit = unit;
    }

    /*
    public class PToEDamageLog : DamageLog
    {
        public static void Log(Enemy enemy, Player player, Damage damage) => AddLog(new PToEDamageLog(enemy, player, damage));

        private PToEDamageLog(Enemy enemy, Player player, Damage damage) : base(enemy, player, damage) {}
    }

    public class EToPDamageLog : DamageLog
    {
        public static void Log(Enemy enemy, Player player, Damage damage) =>
            AddLog(new EToPDamageLog(enemy, player, damage));

        private EToPDamageLog(Enemy enemy, Player player, Damage damage) : base(enemy, player, damage) {}
    } */

    public class GotDamageLog : Log
    {

        private readonly Unit unit;
        private readonly int damage;

        public (Unit, int) GetData => (unit, damage);
        private GotDamageLog(Unit unit, int damage)
        {
            this.unit = unit;
            this.damage = damage;
        }

        public static void Log(Unit unit, int damage) => AddLog(new GotDamageLog(unit, damage));
    }

    public class DamageLog : Log
    {
        private readonly Enemy enemy;
        private readonly Player player;
        private readonly Damage damage;
    
        public (Enemy, Player, Damage) GetData => (enemy, player, damage);

        protected DamageLog(Enemy enemy, Player player, Damage damage)
        {
            this.enemy = enemy;
            this.player = player;
            this.damage = damage;
        }
    }

    public class Log
    {
        public delegate void OnLogDelegate(Log log);
        public static event OnLogDelegate OnLog;
        protected static void AddLog(Log log)
        {
            BattleLog.AddLog(log);
            OnLog?.Invoke(log);
        }

        public static void UnAttach()
        {
            OnLog = null;
        }
    }
}