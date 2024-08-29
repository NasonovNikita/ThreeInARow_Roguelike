using System;
using Battle.Modifiers;
using Battle.UI.ModsDisplaying;
using Core.Singleton;
using UnityEngine;

namespace Battle.Units.Statuses
{
    [Serializable]
    public class Burning : Status
    {
        [SerializeField] private MoveCounter moveCounter;
        private int _dmg = 10;

        public Burning(int moves = 1) => moveCounter =
            CreateChangeableSubSystem(new MoveCounter(moves, true));

        public override Sprite Sprite => ModSpritesContainer.Instance.burning;

        public override string Description =>
            IModIconModifier.SimpleFormatDescription(
                ModDescriptionsContainer.Instance.burning.Value, _dmg);

        public override string SubInfo => moveCounter.SubInfo;
        protected override bool HiddenEndedWork => moveCounter.EndedWork;

        public override void Init(Unit unit)
        {
            moveCounter.OnMove += () => unit.TakeDamage(_dmg);

            base.Init(unit);
        }

        protected override bool HiddenCanConcat(Modifier other) =>
            other is Burning burning &&
            burning.moveCounter.Moves == moveCounter.Moves;

        protected override void HiddenConcat(Modifier other)
        {
            _dmg += ((Burning)other)._dmg;
        }
    }
}