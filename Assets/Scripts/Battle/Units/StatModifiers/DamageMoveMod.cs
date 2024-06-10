using System;
using System.Collections.Generic;
using Battle.Modifiers;
using Other;
using UnityEngine;

namespace Battle.Units.StatModifiers
{
    [Serializable]
    public class DamageMoveMod : DamageMod
    {
        [SerializeField] private MoveCounter moveCounter;

        public DamageMoveMod(int value, int moves, bool save = false) : base(value, save)
        {
            moveCounter = CreateChangeableSubSystem(new MoveCounter(moves));
        }

        protected override List<IChangeAble> ChangeAblesToInitialize => new() { moveCounter };

        public string SubInfo => moveCounter.SubInfo;
        public bool ToDelete => moveCounter.EndedWork || base.ToDelete;

        protected override bool CanConcat(Modifier other)
        {
            return other is DamageMoveMod damageMoveMod &&
                   damageMoveMod.moveCounter.Moves == moveCounter.Moves;
        }
    }
}