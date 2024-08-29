using Battle.Modifiers;
using Battle.UI.ModsDisplaying;
using Core.Singleton;
using Other;
using UnityEngine;

namespace Battle.Units.StatModifiers
{
    public class Frozen : UnitModifier, IIntModifier, IModIconModifier
    {
        private readonly Counter _counter;

        protected override bool HiddenEndedWork => _counter.EndedWork;

        public Frozen(int count, bool save = false) : base(save)
        {
            _counter = CreateChangeableSubSystem(new Counter(count));
        }

        protected override bool HiddenCanConcat(Modifier other) =>
            other is Frozen;

        protected override void HiddenConcat(Modifier other) => 
            _counter.ConcatWith(((Frozen)other)._counter);

        int IIntModifier.Modify(int val) => _counter.Decrease(val);

        public override Sprite Sprite => ModSpritesContainer.Instance.frozen;

        public override string Description =>
            IModIconModifier.SimpleFormatDescription(
                ModDescriptionsContainer.Instance.frozen.Value, _counter.Count);

        public override string SubInfo => _counter.SubInfo;
    }
}