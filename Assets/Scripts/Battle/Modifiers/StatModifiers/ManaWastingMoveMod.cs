using System;
using UnityEngine;

namespace Battle.Modifiers.StatModifiers
{
    [Serializable]
    public class ManaWastingMoveMod : BaseStatModifier
    {
        [SerializeField] private MoveCounter moveCounter;

        public ManaWastingMoveMod(int value, int moves, bool save = false) : base(value, save) => 
            moveCounter = new MoveCounter(moves);

        protected override bool ConcatAbleWith(BaseStatModifier other) =>
            other is ManaWastingMoveMod wastingMoveMod &&
            moveCounter.moves == wastingMoveMod.moveCounter.moves;

        public override Sprite Sprite => throw new NotImplementedException();

        public override string Tag => throw new NotImplementedException();

        public override string Description => throw new NotImplementedException();

        public override string SubInfo => moveCounter.SubInfo;

        public override bool ToDelete => moveCounter.EndedWork;
    }
}