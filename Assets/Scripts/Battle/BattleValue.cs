using System;
using Battle.Units;
using UnityEngine;

namespace Battle
{
    [Serializable]
    public class BattleValue
    {
        [SerializeField] private SourceType sourceType;

        [SerializeField] private StatType statType;
        
        
    }

    internal enum SourceType
    {
        Stat,
        Log,
        Grid
    }
}