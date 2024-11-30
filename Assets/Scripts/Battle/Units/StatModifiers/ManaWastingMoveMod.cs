using System;
using System.Collections.Generic;
using Battle.Modifiers;
using Other;
using UnityEngine;

namespace Battle.Units.StatModifiers
{
    [Serializable]
    public class ManaWastingMoveMod : ManaWastingMod
    {
        [SerializeField] private MoveCounter moveCounter;

        public ManaWastingMoveMod(int value, int moves, bool isSaved = false) : base(value,
            isSaved) =>
            moveCounter = CreateChangeableSubSystem(new MoveCounter(moves));

        protected override List<IChangeAble> ChangeAblesToInitialize =>
            new() { moveCounter };

        public override string SubInfo => moveCounter.SubInfo;

        protected override bool HiddenEndedWork =>
            moveCounter.EndedWork || base.HiddenEndedWork;

        protected override bool HiddenCanConcat(Modifier other) =>
            other is ManaWastingMoveMod mod &&
            mod.moveCounter.Moves == moveCounter.Moves;
    }
}