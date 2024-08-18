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

        public HpDamageMoveMod(int value, int moves, bool save = false) : base(value,
            save)
        {
            moveCounter = CreateChangeableSubSystem(new MoveCounter(moves));
        }


        protected override List<IChangeAble> ChangeAblesToInitialize =>
            new() { moveCounter };

        public override string SubInfo => moveCounter.SubInfo;

        public override bool ToDelete => moveCounter.EndedWork || base.ToDelete;

        protected override bool HiddenCanConcat(Modifier other)
        {
            return other is HpDamageMoveMod damageMoveMod &&
                   damageMoveMod.moveCounter.Moves == moveCounter.Moves;
        }
    }
}