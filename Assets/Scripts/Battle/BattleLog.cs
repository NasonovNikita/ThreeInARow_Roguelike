using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Match3;
using Battle.Units;

namespace Battle
{
    public static class BattleLog
    {
        private static readonly List<Log> Logs = new();
        public static List<T> GetLogs<T>() => Logs.Where(log => log is T).Cast<T>().ToList();

        public static List<Log> GetAllLogs() => Logs;

        public static Log GetLastLog() => Logs[-1];

        public static List<Log> GetLastTurn()
        {
            if (!Logs.Exists(v => v is TurnLog) || Logs.Count(val => val is TurnLog) == 1)
            {
                return Logs;
            }

            int i = Math.Max(Logs.Count - 2, 0);
            while (i >= 0)
            {
                if (Logs[i] is TurnLog) break;
                i--;
            }

            return Logs.ToArray()[i..^1].ToList();
        }

        public static void Clear() => Logs.Clear();

        internal static void AddLog(Log log) => Logs.Add(log);
    }

    public class GridLog : Log
    {
        private readonly Dictionary<GemType, int> _table;
    
        public static void Log(Dictionary<GemType, int> table) => AddLog(new GridLog(table));

        public Dictionary<GemType, int> GetData => _table;

        private GridLog(Dictionary<GemType, int> table) => _table = table;
    }

    public class SpellUsageLog : Log
    {
        private Unit unit;

        private int wasted;

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
        private readonly Unit _unit;
    
        public static void Log(Unit unit) => AddLog(new DeathLog(unit));

        public Unit GetData => _unit;

        private DeathLog(Unit unit) => _unit = unit;
    }

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
    }

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
        private readonly Enemy _enemy;
        private readonly Player _player;
        private readonly Damage _damage;
    
        public (Enemy, Player, Damage) GetData => (_enemy, _player, _damage);

        protected DamageLog(Enemy enemy, Player player, Damage damage)
        {
            _enemy = enemy;
            _player = player;
            _damage = damage;
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