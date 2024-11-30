using Battle.Modifiers;
using Battle.UI.ModsDisplaying;
using Core.Singleton;
using Other;
using UnityEngine;

namespace Battle.Units.StatModifiers
{
    public class Reflection : UnitModifier, IIntModifier, IModIconModifier
    {
        private readonly Counter _counter;

        protected override bool HiddenEndedWork => _counter.EndedWork;

        public Reflection(int count, bool isSaved = false) : base(isSaved)
        {
            _counter = CreateChangeableSubSystem(new Counter(count));
        }
        
        protected override bool HiddenCanConcat(Modifier other) => other is Reflection;

        protected override void HiddenConcat(Modifier other) =>
            _counter.ConcatWith(((Reflection)other)._counter);

        int IIntModifier.Modify(int val)
        {
            BelongingUnit.target.TakeDamage(val - _counter.Decrease(val));

            return val;
        }

        public override Sprite Sprite => ModSpritesContainer.Instance.reflection;

        public override string Description =>
            IModIconModifier.SimpleFormatDescription(
                ModDescriptionsContainer.Instance.reflection.Value, _counter.Count);
        
        public override string SubInfo => _counter.SubInfo;
    }
}