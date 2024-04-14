using System;
using Battle.Units;
using UnityEngine;

namespace Battle.Modifiers.Statuses
{
    [Serializable]
    public class RandomIgnition : Status
    {
        [SerializeField] private int chance;
        [SerializeField] private int burningMoves;

        [SerializeField] private MoveCounter moveCounter;

        public RandomIgnition(int chance, int moves, int burningMoves = 1)
        {
            this.chance = chance;
            this.burningMoves = burningMoves;

            moveCounter = CreateChangeableSubSystem(new MoveCounter(moves));
        }
        
        public override Sprite Sprite => throw new NotImplementedException();
        public override string Description => throw new NotImplementedException();
        public override string SubInfo => moveCounter.SubInfo;
        public override bool ToDelete => moveCounter.EndedWork || chance == 0;

        public override void Init(Unit unit)
        {
            moveCounter.OnMove += () =>
            {
                if (Other.Tools.Random.RandomChance(chance)) unit.AddStatus(new Burning(burningMoves));
            };
            
            base.Init(unit);
        }

        protected override bool CanConcat(Modifier other) =>
            other is RandomIgnition ignition &&
            ignition.moveCounter.Moves == moveCounter.Moves &&
            ignition.burningMoves == burningMoves;

        public override void Concat(Modifier other) =>
            chance += ((RandomIgnition)other).chance;
    }
}