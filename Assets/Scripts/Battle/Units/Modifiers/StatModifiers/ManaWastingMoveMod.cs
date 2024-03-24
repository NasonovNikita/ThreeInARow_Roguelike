using System;
using UI.Battle;
using UnityEngine;

namespace Battle.Units.Modifiers.StatModifiers
{
    [Serializable]
    public class ManaWastingMoveMod : IStatModifier, IConcatAble, IModIconAble, ISaveAble
    {
        [SerializeField] private int value;
        [SerializeField] private MoveCounter moveCounter;

        public ManaWastingMoveMod(int value, int moves, bool save = false)
        {
            this.value = value;
            moveCounter = new MoveCounter(moves);
            Save = save;
        }
        
        int IStatModifier.Modify(int val) => val + value;

        public bool ConcatAbleWith(IConcatAble other) =>
            other is ManaWastingMoveMod wastingMoveMod &&
            wastingMoveMod.moveCounter.moves == moveCounter.moves;

        public void Concat(IConcatAble other) =>
            value += ((ManaWastingMoveMod)other).value;

        public Sprite Sprite => throw new NotImplementedException();

        public string Tag => throw new NotImplementedException();

        public string Description => throw new NotImplementedException();

        public string SubInfo => moveCounter.SubInfo;

        public bool ToDelete => moveCounter.EndedWork;

        public bool Save { get; }
    }
}