using System;
using UnityEngine;
using Grid = Battle.Match3.Grid;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Battle
{
    [Serializable]
    public class Condition
    {
        [SerializeField] private CondType type;

        [SerializeField] private int value;

        [SerializeField] private CompareMethod compare;

        [SerializeField] private GemType gem;

        [SerializeField] private UnitType unit;

        [SerializeField] private UnitStat stat;

        public bool Use()
        {
            BattleManager manager = Object.FindFirstObjectByType<BattleManager>();
            switch (type)
            {
                case CondType.Grid:
                    Grid grid = manager.grid;
                    return Compare(grid.destroyed.ContainsKey(gem) ? grid.destroyed[gem] : 0, value);
                case CondType.Unit:
                    Unit checkUnit = unit switch
                    {
                        UnitType.Player => manager.player,
                        UnitType.Target => manager.target,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    Stat checkStat = stat switch
                    {
                        UnitStat.Hp => checkUnit.hp,
                        UnitStat.Mana => checkUnit.mana,
                        //UnitStat.Damage => checkUnit.damage,
                        UnitStat.Damage => checkUnit.phDmg,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    return Compare((int)checkStat, value);
                case CondType.Random:
                    return Compare(Random.Range(0, 100), value);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool Compare(int a, int b)
        {
            return compare switch
            {
                CompareMethod.Bigger => a > b,
                CompareMethod.AtLeast => a >= b,
                CompareMethod.Equals => a == b,
                CompareMethod.NotMore => a <= b,
                CompareMethod.Less => a < b,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public enum CondType
    {
        Grid,
        Unit,
        Random
    }

    public enum UnitType
    {
        Player,
        Target
    }

    public enum UnitStat
    {
        Hp,
        Mana,
        Damage
    }

    public enum CompareMethod
    {
        Bigger,
        AtLeast,
        Equals,
        NotMore,
        Less
    }
}