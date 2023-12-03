using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Units;
using Battle.Units.Enemies;

namespace Battle
{
    public static class BattleLog
    {
        private static readonly List<ILog> Logs = new();
        public static List<T> GetLogs<T>() => Logs.Where(log => log is T).Cast<T>().ToList();

        public static List<ILog> GetAllLogs() => Logs;

        public static ILog GetLastLog() => Logs[-1];

        public static List<ILog> GetLastTurn()
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

        internal static void AddLog(ILog log) => Logs.Add(log);
    }

    public class GridLog : ILog
    {
        private readonly Dictionary<GemType, int> _table;
    
        public static void Log(Dictionary<GemType, int> table) => ILog.AddLog(new GridLog(table));

        public Dictionary<GemType, int> GetData() => _table;

        private GridLog(Dictionary<GemType, int> table) => _table = table;
    }
    
    public class TurnLog : ILog
    {
        public static void Log() => ILog.AddLog(new TurnLog());
        
    }

    public class DeathLog : ILog
    {
        private readonly Unit _unit;
    
        public static void Log(Unit unit) => ILog.AddLog(new DeathLog(unit));

        public Unit GetData() => _unit;

        private DeathLog(Unit unit) => _unit = unit;
    }

    public class PToEDamageLog : DamageLog, ILog
    {
        public static void Log(Enemy enemy, Player player, Damage damage) => ILog.AddLog(new PToEDamageLog(enemy, player, damage));

        private PToEDamageLog(Enemy enemy, Player player, Damage damage) : base(enemy, player, damage) {}
    }

    public class EToPDamageLog : DamageLog, ILog
    {
        public static void Log(Enemy enemy, Player player, Damage damage) => ILog.AddLog(new EToPDamageLog(enemy, player, damage));

        private EToPDamageLog(Enemy enemy, Player player, Damage damage) : base(enemy, player, damage) {}
    }

    public class DamageLog
    {
        private readonly Enemy _enemy;
        private readonly Player _player;
        private readonly Damage _damage;
    
        public (Enemy, Player, Damage) GetData() => (_enemy, _player, _damage);

        protected DamageLog(Enemy enemy, Player player, Damage damage)
        {
            _enemy = enemy;
            _player = player;
            _damage = damage;
        }
    }

    public interface ILog
    {
        public delegate void OnLogDelegate(ILog log);

        public static event OnLogDelegate OnLog;
        protected internal static void AddLog(ILog log)
        {
            BattleLog.AddLog(log);
            OnLog?.Invoke(log);
        }
    }
}