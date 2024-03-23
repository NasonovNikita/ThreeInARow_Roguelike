using UnityEngine;

namespace Battle.Units.Modifiers.Statuses
{
    public class Stun : MoveCounter, IModifier
    {
        public Stun(int moves, bool delay = false, bool permanent = false) : base(moves, delay, permanent) {}

        public Sprite Sprite => throw new System.NotImplementedException();

        public string Description => throw new System.NotImplementedException();

        bool IModifier.ConcatAble(IModifier other) => other is Stun;

        protected override void Move()
        {
            unitBelong.WasteAllMoves();
            base.Move();
        }
    }
}