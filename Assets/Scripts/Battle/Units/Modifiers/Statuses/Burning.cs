using UnityEngine;

namespace Battle.Units.Modifiers.Statuses
{
    public class Burning : MoveCounter, IModifier
    {
        private readonly int dmg;

        public Burning(int dmg, int moves, bool delay = false, bool permanent = false) :
            base(moves, delay, permanent) =>
            this.dmg = dmg;

        public Sprite Sprite => throw new System.NotImplementedException();

        public string Description => throw new System.NotImplementedException();

        protected override void Move()
        {
            if (!delay) unitBelong.TakeDamage(dmg);
            base.Move();
        }

        bool IModifier.ConcatAble(IModifier other)
        {
            throw new System.NotImplementedException();
        }
    }
}