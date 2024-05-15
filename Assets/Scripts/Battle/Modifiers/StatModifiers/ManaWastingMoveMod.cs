using System;
using System.Collections.Generic;
using Other;
using UnityEngine;

namespace Battle.Modifiers.StatModifiers
{
    [Serializable]
    public class ManaWastingMoveMod : ManaWastingMod
    {
        [SerializeField] private MoveCounter moveCounter;

        public ManaWastingMoveMod(int value, int moves, bool save = false) : base(value, save) => 
            moveCounter = CreateChangeableSubSystem(new MoveCounter(moves));

        protected bool ConcatAbleWith(StatModifier other) =>
            other is ManaWastingMoveMod wastingMoveMod &&
            moveCounter.Moves == wastingMoveMod.moveCounter.Moves;

        protected override List<IChangeAble> ChangeAblesToInitialize => new() { moveCounter };

        public override string SubInfo => moveCounter.SubInfo;
        public override bool ToDelete => moveCounter.EndedWork || base.ToDelete;
        
        protected override bool CanConcat(Modifier other) => 
            other is ManaWastingMoveMod mod &&
            mod.moveCounter.Moves == moveCounter.Moves;
    }
}