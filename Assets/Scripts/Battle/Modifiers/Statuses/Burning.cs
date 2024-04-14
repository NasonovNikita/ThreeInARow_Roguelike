using System;
using Battle.Units;
using UnityEngine;

namespace Battle.Modifiers.Statuses
{
    [Serializable]
    public class Burning : Status
    {
        private int dmg = 20;
        [SerializeField] private MoveCounter moveCounter;

        public Burning(int moves = 1) => 
            moveCounter = CreateChangeableSubSystem(new MoveCounter(moves, true));

        public override Sprite Sprite => throw new NotImplementedException();
        public override string Description => throw new NotImplementedException();
        public override string SubInfo => moveCounter.SubInfo;
        public override bool ToDelete => moveCounter.EndedWork;

        public override void Init(Unit unit)
        {
            moveCounter.OnMove += () => unit.TakeDamage(dmg);
            
            base.Init(unit);
        }

        protected override bool CanConcat(Modifier other) => 
            other is Burning burning && 
            burning.moveCounter.Moves == moveCounter.Moves;

        public override void Concat(Modifier other) => 
            dmg += ((Burning)other).dmg;
    }
}