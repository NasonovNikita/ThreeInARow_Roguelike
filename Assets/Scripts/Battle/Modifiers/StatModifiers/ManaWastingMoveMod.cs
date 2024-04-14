using System;
using UnityEngine;

namespace Battle.Modifiers.StatModifiers
{
    [Serializable]
    public class ManaWastingMoveMod : ValuedStatModifier
    {
        [SerializeField] private MoveCounter moveCounter;

        public ManaWastingMoveMod(int value, int moves, bool save = false) : base(value, save) => 
            moveCounter = CreateChangeableSubSystem(new MoveCounter(moves));

        protected bool ConcatAbleWith(StatModifier other) =>
            other is ManaWastingMoveMod wastingMoveMod &&
            moveCounter.Moves == wastingMoveMod.moveCounter.Moves;

        public override Sprite Sprite => throw new NotImplementedException();
        public override string Description => throw new NotImplementedException();
        public override string SubInfo => moveCounter.SubInfo;
        public override bool ToDelete => moveCounter.EndedWork;
        
        protected override bool CanConcat(Modifier other) => 
            other is ManaWastingMoveMod mod &&
            mod.moveCounter.Moves == moveCounter.Moves;
    }
}