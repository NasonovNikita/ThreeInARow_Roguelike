using System;
using System.Collections.Generic;
using Battle.Modifiers;
using Battle.UI.ModsDisplaying;
using Core.Singleton;
using Other;
using UnityEngine;

namespace Battle.Units.Statuses
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

        public override Sprite Sprite => ModifierSpritesContainer.Instance.randomIgnition;

        public override string Description =>
            IModIconModifier.FormatDescriptionByKeys(
                ModDescriptionsContainer.Instance.randomIgnition.Value,
                new Dictionary<string, object>
                {
                    { "chance", chance },
                    { "burningMoves", burningMoves }
                });

        public override string SubInfo => moveCounter.SubInfo;
        public override bool ToDelete => moveCounter.EndedWork || chance == 0;

        public override void Init(Unit unit)
        {
            moveCounter.OnMove += () =>
            {
                if (Tools.Random.RandomChance(chance))
                    unit.Statuses.Add(new Burning(burningMoves));
            };

            base.Init(unit);
        }

        protected override bool HiddenCanConcat(Modifier other)
        {
            return other is RandomIgnition ignition &&
                   ignition.moveCounter.Moves == moveCounter.Moves &&
                   ignition.burningMoves == burningMoves;
        }

        protected override void HiddenConcat(Modifier other)
        {
            chance += ((RandomIgnition)other).chance;
        }
    }
}