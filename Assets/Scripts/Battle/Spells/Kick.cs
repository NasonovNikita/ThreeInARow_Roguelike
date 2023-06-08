using System;
using Battle.Units;
using Battle.Units.Enemies;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Kick", menuName = "Spells/Kick")]
    public class Kick : Spell
    {
        [SerializeField] private ActiveAction damageAction;
        public override void Cast()
        {
            if (CantCast()) return;
        
            unit.mana -= manaCost;
            Unit second = unit switch
            {
                Player => BattleManager.target,
                Enemy => BattleManager.player,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            damageAction.Use(second);
        }
    }
}