using Battle.Units;
using UnityEngine;

namespace Battle.Modifiers.Statuses
{
    public class Stun : Status
    {
        private readonly MoveCounter moveMod;

        public Stun(int moves, bool save = false) : base(save)
        {
            moveMod = CreateChangeableSubSystem(new MoveCounter(moves));
        }

        public override Sprite Sprite => throw new System.NotImplementedException();
        public override string Description => throw new System.NotImplementedException();
        public override string SubInfo => moveMod.SubInfo;
        public override bool ToDelete => moveMod.EndedWork;

        public override void Init(Unit unit) => moveMod.OnMove += unit.WasteAllMoves;
        protected override bool CanConcat(Modifier other) => other is Stun;

        public override void Concat(Modifier other)
        {
            moveMod.Concat(((Stun)other).moveMod);
        }
    }
}