using System;
using UnityEngine;

namespace Battle
{
    [Serializable]
    public class ActiveAction
    {
        [SerializeField] private ActionType type;
        
        [SerializeField] private int value;
        
        [SerializeField] private int stunMoves;

        public void Use(Unit unit)
        {
            switch (type)
            {
                case ActionType.Stun:
                    Modifier.CreateModifier(stunMoves, unit, ModType.Stun);
                    break;
                case ActionType.Damage:
                    DamageLog.Log(null, unit, value);
                    unit.DoDamage(value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum ActionType
    {
        Stun,
        Damage
    }
}