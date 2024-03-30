using System;
using UnityEngine;

namespace Battle.Modifiers.StatModifiers
{
    [Serializable]
    public class DamageMoveMod : BaseStatModifier
    {
        [SerializeField] private MoveCounter moveCounter;

        public DamageMoveMod(int value, int moves, bool save = false) : base(value, save) => 
            moveCounter = new MoveCounter(moves);

        public override Sprite Sprite => throw new NotImplementedException();

        public override string Tag => throw new NotImplementedException();

        public override string Description => throw new NotImplementedException();

        public override string SubInfo => moveCounter.SubInfo;

        public override bool ToDelete => moveCounter.EndedWork;

        protected override bool ConcatAbleWith(BaseStatModifier other) =>
            other is DamageMoveMod damageMoveMod &&
            moveCounter.moves == damageMoveMod.moveCounter.moves;
    }
}