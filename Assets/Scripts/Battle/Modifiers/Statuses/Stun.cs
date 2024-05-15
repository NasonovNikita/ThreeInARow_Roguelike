using Battle.Units;
using Core.Singleton;
using UI.Battle.ModsDisplaying;
using UnityEngine;

namespace Battle.Modifiers.Statuses
{
    public class Stun : Status
    {
        private readonly MoveCounter moveMod;

        public Stun(int moves, bool save = false) : base(save)
        {
            moveMod = CreateChangeableSubSystem(new MoveCounter(moves));
        }

        public override Sprite Sprite => ModifierSpritesContainer.Instance.stun;
        public override string Description =>
            SimpleFormatDescription(ModDescriptionsContainer.Instance.stun.Value, moveMod.Moves);
        public override string SubInfo => moveMod.SubInfo;
        public override bool ToDelete => moveMod.EndedWork;

        public override void Init(Unit unit) => moveMod.OnMove += unit.WasteAllMoves;
        protected override bool CanConcat(Modifier other) => other is Stun;

        public override void Concat(Modifier other)
        {
            moveMod.Concat(((Stun)other).moveMod);
        }
    }
}