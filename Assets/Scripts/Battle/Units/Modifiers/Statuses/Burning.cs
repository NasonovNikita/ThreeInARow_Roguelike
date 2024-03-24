using System;
using UnityEngine;

namespace Battle.Units.Modifiers.Statuses
{
    [Serializable]
    public class Burning : Status, IConcatAble
    {
        [SerializeField] private int dmg;
        [SerializeField] private MoveCounter moveCounter;

        public Burning(int dmg, int moves)
        {
            this.dmg = dmg;
            moveCounter = new MoveCounter(moves, true);
        }

        public override Sprite Sprite => throw new System.NotImplementedException();

        public override string Tag => throw new System.NotImplementedException();

        public override string Description => throw new System.NotImplementedException();

        public override string SubInfo => throw new System.NotImplementedException();

        public override bool ToDelete => throw new System.NotImplementedException();

        public bool ConcatAbleWith(IConcatAble other) =>
            other is Burning burning && burning.moveCounter.moves == moveCounter.moves;

        public void Concat(IConcatAble other) => dmg += ((Burning)other).dmg;
    }
}