using Battle.Modifiers;
using Battle.UI.ModsDisplaying;
using Core.Singleton;
using UnityEngine;

namespace Battle.Units.Statuses
{
    public class Stun : Status
    {
        private readonly MoveCounter _moveMod;

        public Stun(int moves, bool isSaved = false) : base(isSaved) =>
            _moveMod = CreateChangeableSubSystem(new MoveCounter(moves));

        public override Sprite Sprite => ModSpritesContainer.Instance.stun;

        public override string Description =>
            IModIconModifier.SimpleFormatDescription(
                ModDescriptionsContainer.Instance.stun.Value,
                _moveMod.Moves);

        public override string SubInfo => _moveMod.SubInfo;
        protected override bool HiddenEndedWork => _moveMod.EndedWork;

        protected override bool HiddenCanConcat(Modifier other) => other is Stun;

        protected override void HiddenConcat(Modifier other)
        {
            _moveMod.Concat(((Stun)other)._moveMod);
        }
    }
}