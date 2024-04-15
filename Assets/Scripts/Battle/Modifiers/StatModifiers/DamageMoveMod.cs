using System;
using UnityEngine;

namespace Battle.Modifiers.StatModifiers
{
    [Serializable]
    public class DamageMoveMod : DamageMod
    {
        [SerializeField] private MoveCounter moveCounter;

        public DamageMoveMod(int value, int moves, bool save = false) : base(value, save) => 
            moveCounter = CreateChangeableSubSystem(new MoveCounter(moves));
        public override string SubInfo => moveCounter.SubInfo;
        public override bool ToDelete => moveCounter.EndedWork || base.ToDelete;
        protected override bool CanConcat(Modifier other) => 
            other is DamageMoveMod damageMoveMod &&
            damageMoveMod.moveCounter.Moves == moveCounter.Moves;
    }
}