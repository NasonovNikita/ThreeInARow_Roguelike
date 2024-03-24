using System;
using UI.Battle;
using UnityEngine;

namespace Battle.Units.Modifiers.StatModifiers
{
    [Serializable]
    public class DamageMoveMod : IStatModifier, IConcatAble, ISaveAble, IModIconAble
    {
        [SerializeField] private int value;
        [SerializeField] private MoveCounter moveCounter;

        public DamageMoveMod(int value, int moves, bool save = false)
        {
            this.value = value;
            moveCounter = new MoveCounter(moves);
            Save = save;
        }

        public Sprite Sprite => throw new NotImplementedException();

        public string Tag => throw new NotImplementedException();

        public string Description => throw new NotImplementedException();

        public string SubInfo => moveCounter.SubInfo;

        public bool ToDelete => moveCounter.EndedWork;

        int IStatModifier.Modify(int val) => val + value;

        public bool ConcatAbleWith(IConcatAble other) =>
            other is DamageMoveMod damageMoveMod && damageMoveMod.moveCounter.moves == moveCounter.moves;

        public void Concat(IConcatAble other) =>
            moveCounter.moves += ((DamageMoveMod)other).moveCounter.moves;

        public bool Save { get; }
    }
}