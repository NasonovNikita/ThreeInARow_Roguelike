using System;
using System.Diagnostics.CodeAnalysis;
using Battle.Units;
using Battle.Units.Enemies;
using UnityEngine;

namespace Battle
{
    [Serializable]
    [SuppressMessage("ReSharper", "Unity.NoNullPropagation")]
    public class Condition
    {
        [SerializeField] private ModOrAction modOrAction;
        
        [SerializeField] private Modifier mod;

        [SerializeField] public ActiveAction action;
        
        [SerializeField] private CondType condType = CondType.EveryTurn;

        [SerializeField] private StatType statType = StatType.Hp;

        [SerializeField] private CompareMethod compareMethod = CompareMethod.AtLeast;

        [SerializeField] private int value;

        [SerializeField] private TargetType targetType;

        [SerializeField] private UnitType unitType;
        
        private Unit _attachedUnit;

        private bool _checkedStat;

        public void UseToTargets()
        {
            switch (targetType)
            {
                case TargetType.Me:
                    Use(_attachedUnit);
                    break;
                case TargetType.Enemy:
                    switch (_attachedUnit)
                    {
                        case Player:
                            Use(BattleManager.target);
                            break;
                        case Enemy:
                            Use(BattleManager.player);
                            break;
                    }
                    break;
                case TargetType.AllEnemies: // Only used when attached to a Player
                    foreach (var enemy in BattleManager.enemies)
                    {
                        Use(enemy);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Use(Unit unit)
        {
            switch (modOrAction)
            {
                case ModOrAction.Mod:
                    mod.Use(unit);
                    break;
                case ModOrAction.Action:
                    action.Use(unit);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Check(Log log)
        {
            Stat stat;
            switch (condType)
            {
                case CondType.EveryTurn:
                    if (log is TurnLog) UseToTargets();
                    break;
                case CondType.Stat:
                    stat = _attachedUnit.StatByType(statType);
                    switch (_checkedStat)
                    {
                        case false when Compare((int)stat, value):
                            UseToTargets();
                            _checkedStat = true;
                            break;
                        case true when !Compare((int)stat, value) && modOrAction is ModOrAction.Mod:
                            mod.Stop();
                            break;
                    }
                    break;
                case CondType.GottenDamage:
                    if (log is DamageLog gottenDamage && gottenDamage.Data().Item2?.type == unitType &&
                        Compare(gottenDamage.Data().Item3, value))
                    {
                        _attachedUnit = gottenDamage.Data().Item2;
                        UseToTargets();
                    }
                    break;
                case CondType.DoneDamage:
                    if (log is DamageLog doneDamage && doneDamage.Data().Item1?.type == unitType &&
                        Compare(doneDamage.Data().Item3, value)) UseToTargets();
                    break;
                case CondType.OnceAtTheBeginning:
                    if (log is BattleBeginLog) UseToTargets();
                    break;
                case CondType.EveryTurnStat:
                    if (log is TurnLog)
                    {
                        stat = _attachedUnit.StatByType(statType);
                        if (Compare((int) stat, value)) UseToTargets();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Init(Unit unit)
        {
            _attachedUnit = unit;
            Log.logger += Check;
        }

        private bool Compare(int a, int b)
        {
            return compareMethod switch
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
        OnceAtTheBeginning,
        EveryTurn,
        Stat,
        EveryTurnStat,
        GottenDamage,
        DoneDamage
    }

    internal enum ModOrAction
    {
        Mod,
        Action
    }

    public enum CompareMethod
    {
        Bigger,
        AtLeast,
        Equals,
        NotMore,
        Less
    }

    public enum TargetType
    {
        Me,
        Enemy,
        AllEnemies
    }
}