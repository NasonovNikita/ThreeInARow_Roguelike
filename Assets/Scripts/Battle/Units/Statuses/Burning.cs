using System;
using Battle.Modifiers;
using Core.Singleton;
using UI.Battle.ModsDisplaying;
using UnityEngine;

namespace Battle.Units.Statuses
{
    [Serializable]
    public class Burning : Status
    {
        [SerializeField] private MoveCounter moveCounter;
        private int dmg = 10;

        public Burning(int moves = 1)
        {
            moveCounter = CreateChangeableSubSystem(new MoveCounter(moves, true));
        }

        public override Sprite Sprite => ModifierSpritesContainer.Instance.burning;

        public override string Description =>
            IModIconModifier.SimpleFormatDescription(ModDescriptionsContainer.Instance.burning.Value, dmg);

        public override string SubInfo => moveCounter.SubInfo;
        public override bool ToDelete => moveCounter.EndedWork;

        public override void Init(Unit unit)
        {
            moveCounter.OnMove += () => unit.TakeDamage(dmg);

            base.Init(unit);
        }

        protected override bool CanConcat(Modifier other)
        {
            return other is Burning burning &&
                   burning.moveCounter.Moves == moveCounter.Moves;
        }

        public override void Concat(Modifier other)
        {
            dmg += ((Burning)other).dmg;
        }
    }
}