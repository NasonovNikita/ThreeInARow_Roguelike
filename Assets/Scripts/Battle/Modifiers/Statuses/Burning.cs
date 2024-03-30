using System;
using Battle.Units;
using UnityEngine;

namespace Battle.Modifiers.Statuses
{
    [Serializable]
    public class Burning : Status, IConcatAble
    {
        private const int Dmg = 20;
        [SerializeField] private MoveCounter moveCounter;

        public Burning(int moves = 1)
        {
            moveCounter = new MoveCounter(moves, true);
        }

        public override Sprite Sprite => throw new NotImplementedException();

        public override string Tag => throw new NotImplementedException();

        public override string Description => throw new NotImplementedException();

        public override string SubInfo => moveCounter.SubInfo;

        public override bool ToDelete => moveCounter.EndedWork;

        public override void Init(Unit unit)
        {
            moveCounter.onMove = () => unit.TakeDamage(Dmg);
            
            base.Init(unit);
        }

        public bool ConcatAbleWith(IConcatAble other) =>
            other is Burning;

        public void Concat(IConcatAble other) =>
            moveCounter.moves += ((Burning)other).moveCounter.moves;
    }
}