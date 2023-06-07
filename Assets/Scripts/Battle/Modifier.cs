using System;
using System.Collections.Generic;
using System.Linq;
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

        [SerializeField] private StatAffect statAffect;

        private Unit _unitRelated;

        public static void CreateModifier(int moves, Unit unitRelated, ModType type, float value = 1,
            FuncAffect funcAffect = FuncAffect.None, StatAffect statAffect = StatAffect.None)
        {
            
        }
        
        

        public Modifier(int moves, Unit unitRelated, ModType type, float value = 1,
            FuncAffect funcAffect = FuncAffect.None, StatAffect statAffect = StatAffect.None)
        {
            this.moves = moves;
            _unitRelated = unitRelated;
            this.type = type;
            this.value = value;
            this.funcAffect = funcAffect;
            this.statAffect = statAffect;
        }
        public void Move(Log log)
        {
            if (moves != 0) moves -= 1;
        }

        public void Use()
        {
            if (Type is ModType.Stun) _unitRelated.statusModifiers.Add(this);
            else
            {
                Stat stat;
            }
        }
    }
    
    public enum FuncAffect
    {
        Add,
        Sub,
        Get,
        None
    }
    
    public enum ModType
    {
        Add,
        Mul,
        Stun
    }

    public enum StatAffect
    {
        Hp,
        Mana,
        Damage,
        None
    }
}