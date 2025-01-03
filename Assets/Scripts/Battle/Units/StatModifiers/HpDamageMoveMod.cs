using System;
using System.Collections.Generic;
using Battle.Modifiers;
using Other;
using UnityEngine;

namespace Battle.Units.StatModifiers
{
    [Serializable]
    public class HpDamageMoveMod : HpDamageMod
    {
        [SerializeField] private MoveCounter moveCounter;

        public HpDamageMoveMod(int value, int moves, bool isSaved = false) : base(value,
            isSaved) =>
            moveCounter = CreateChangeableSubSystem(new MoveCounter(moves));


        protected override List<IChangeAble> ChangeAblesToInitialize =>
            new() { moveCounter };

        public override string SubInfo => moveCounter.SubInfo;

        protected override bool HiddenEndedWork =>
            moveCounter.EndedWork || base.HiddenEndedWork;

        protected override bool HiddenCanConcat(Modifier other) =>
            other is HpDamageMoveMod damageMoveMod &&
            damageMoveMod.moveCounter.Moves == moveCounter.Moves;
    }
}