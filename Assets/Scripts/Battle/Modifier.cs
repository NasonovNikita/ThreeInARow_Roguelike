using System;
using System.Collections.Generic;
using Battle.Units;
using UnityEngine;

namespace Battle
{
    [Serializable]
    public class Modifier
    {
        public static List<Modifier> mods = new();

        [SerializeField] private int moves;

        [SerializeField] private ModType type;

        public ModType Type => type;

        [SerializeField] private float value;
        
        public float Value => moves != 0 ? value : 0;
        
        [SerializeField] private FuncAffect funcAffect;

        [SerializeField] private UnitStat statAffect;

        private bool _connected;

        public static void CreateModifier(int moves, Unit unit, ModType type, float value = 1,
            FuncAffect funcAffect = FuncAffect.Add, UnitStat statAffect = UnitStat.Hp)
        {
            Modifier mod = new Modifier(moves, type, value, funcAffect, statAffect);
            mod.Use(unit);
        }
        
        public Modifier(int moves, ModType type, float value = 1,
            FuncAffect funcAffect = FuncAffect.Add, UnitStat statAffect = UnitStat.Hp)
        {
            this.moves = moves;
            this.type = type;
            this.value = value;
            this.funcAffect = funcAffect;
            this.statAffect = statAffect;
        }
        
        public void Move(Log log)
        {
            if (moves != 0 && log is TurnLog) moves -= 1;
        }

        public void Use(Unit unit)
        {
            if (!_connected) Log.logger += Move;
            _connected = true;
            if (Type is ModType.Stun) unit.statusModifiers.Add(this);
            else
            {
                Stat stat = unit.StatByType(statAffect);
                
                stat.AddMod(this, funcAffect);
            }
        }

        public void Stop()
        {
            moves = 0;
        }
    }
    
    public enum FuncAffect
    {
        Add,
        Sub,
        Get
    }
    
    public enum ModType
    {
        Add,
        Mul,
        Stun
    }
}