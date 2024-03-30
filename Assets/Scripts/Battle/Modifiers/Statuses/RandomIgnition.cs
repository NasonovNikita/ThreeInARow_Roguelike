using System;
using Battle.Units;
using UnityEngine;

namespace Battle.Modifiers.Statuses
{
    [Serializable]
    public class RandomIgnition : Status, IConcatAble
    {
        [SerializeField] private int chance;
        [SerializeField] private int burningMoves;

        [SerializeField] private MoveCounter moveCounter;

        public RandomIgnition(int chance, int moves, int burningMoves = 1)
        {
            this.chance = chance;
            this.burningMoves = burningMoves;

            moveCounter = new MoveCounter(moves);
        }
        
        public override Sprite Sprite => throw new NotImplementedException();

        public override string Tag => throw new NotImplementedException();

        public override string Description => throw new NotImplementedException();

        public override string SubInfo => moveCounter.SubInfo;

        public override bool ToDelete => moveCounter.EndedWork || chance == 0;

        public override void Init(Unit unit)
        {
            moveCounter.onMove = () =>
            {
                if (Other.Tools.Random.RandomChance(chance)) unit.AddStatus(new Burning(burningMoves));
            };
            
            base.Init(unit);
        }

        public bool ConcatAbleWith(IConcatAble other) =>
            other is RandomIgnition ignition &&
            burningMoves == ignition.burningMoves;

        public void Concat(IConcatAble other) =>
            chance += ((RandomIgnition)other).chance;
    }
}