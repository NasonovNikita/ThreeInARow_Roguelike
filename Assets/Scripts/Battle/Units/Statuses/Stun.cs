using Battle.Modifiers;
using Battle.UI.ModsDisplaying;
using Core.Singleton;
using UnityEngine;

namespace Battle.Units.Statuses
{
    public class Stun : Status
    {
        private readonly MoveCounter _moveMod;

        public Stun(int moves, bool save = false) : base(save)
        {
            _moveMod = CreateChangeableSubSystem(new MoveCounter(moves));
        }

        public override Sprite Sprite => ModifierSpritesContainer.Instance.stun;

        public override string Description =>
            IModIconModifier.SimpleFormatDescription(
                ModDescriptionsContainer.Instance.stun.Value,
                _moveMod.Moves);

        public override string SubInfo => _moveMod.SubInfo;
        public override bool ToDelete => _moveMod.EndedWork;

        protected override bool HiddenCanConcat(Modifier other)
        {
            return other is Stun;
        }

        protected override void HiddenConcat(Modifier other)
        {
            _moveMod.Concat(((Stun)other)._moveMod);
        }
    }
}