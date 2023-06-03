using System.Collections.Generic;
using System.Linq;

public static class BattleLog
{
    private static readonly List<ILog> Logs = new();

    public static List<T> GetLogs<T>()
    {
        return Logs.Where(log => log is T).Cast<T>().ToList();
    }

    public static List<ILog> GetAllLogs()
    {
        return Logs;
    }

    public static ILog GetLastLog()
    {
        return Logs[-1];
    }

    public static void Clear()
    {
        Logs.Clear();
    }

    internal static void AddLog(ILog log)
    {
        Logs.Add(log);
    }
}

public class GridLog : ILog
{
    private readonly Dictionary<GemType, int> _table;
    
    public static void Log(Dictionary<GemType, int> table)
    {
        var log = new GridLog(table);
        ILog.AddLog(log);
    }

    public Dictionary<GemType, int> GetData()
    {
        return _table;
    }

    private GridLog(Dictionary<GemType, int> table)
    {
        _table = table;
    }
}

public class DeathLog : ILog
{
    private readonly Unit _unit;
    
    public static void Log(Unit unit)
    {
        var log = new DeathLog(unit);
        ILog.AddLog(log);
    }

    public Unit GetData()
    {
        return _unit;
    }

    private DeathLog(Unit unit)
    {
        _unit = unit;
    }
}

public class PToEDamageLog : DamageLog, ILog
{
    public static void Log(Enemy enemy, Player player, int damage)
    {
        var log = new PToEDamageLog(enemy, player, damage);
        ILog.AddLog(log);
    }

    private PToEDamageLog(Enemy enemy, Player player, int damage) : base(enemy, player, damage) {}
}

public class EToPDamageLog : DamageLog, ILog
{
    public static void Log(Enemy enemy, Player player, int damage)
    {
        var log = new EToPDamageLog(enemy, player, damage);
        ILog.AddLog(log);
    }

    private EToPDamageLog(Enemy enemy, Player player, int damage) : base(enemy, player, damage) {}
}

public class DamageLog
{
    private readonly Enemy _enemy;
    private readonly Player _player;
    private readonly int _damage;
    
    public (Enemy, Player, int) GetData()
    {
        return (_enemy, _player, _damage);
    }

    protected DamageLog(Enemy enemy, Player player, int damage)
    {
        _enemy = enemy;
        _player = player;
        _damage = damage;
    }
}

public interface ILog
{
    protected internal static void AddLog(ILog log)
    {
        BattleLog.AddLog(log);
    }
}