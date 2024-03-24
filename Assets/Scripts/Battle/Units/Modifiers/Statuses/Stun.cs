using UI.Battle;
using UnityEngine;

namespace Battle.Units.Modifiers.Statuses
{
    public class Stun : Status, IConcatAble
    {
        private readonly MoveCounter mod;

        public Stun(int moves) => mod = new MoveCounter(moves);

        public override Sprite Sprite => throw new System.NotImplementedException();
        public override string Tag => throw new System.NotImplementedException();
        public override string Description => throw new System.NotImplementedException();
        public override string SubInfo => ModIcon.EmptyInfo;
        public override bool ToDelete => mod.EndedWork;

        public override void Init(Unit unit) => mod.onMove = unit.WasteAllMoves;
        public bool ConcatAbleWith(IConcatAble other) => other is Stun;

        public void Concat(IConcatAble other) => mod.Concat(((Stun)other).mod);
    }
}