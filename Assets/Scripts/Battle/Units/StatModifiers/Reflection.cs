using Battle.Modifiers;
using Battle.UI.ModsDisplaying;
using Core.Singleton;
using UnityEngine;

namespace Battle.Units.StatModifiers
{
    public class Reflection : OneTurnCounter, IIntModifier, IModIconModifier
    {
        public Reflection(int count, bool isSaved = false) : base(count, isSaved)
        { }

        protected override bool HiddenCanConcat(Modifier other) => other is Reflection;

        protected override void HiddenConcat(Modifier other) =>
            Counter.ConcatWith(((Reflection)other).Counter);

        int IIntModifier.Modify(int val)
        {
            var unreflected = Counter.Decrease(val);
            
            BelongingUnit.target.TakeDamage(val - unreflected);

            return unreflected;
        }

        public override Sprite Sprite => ModSpritesContainer.Instance.reflection;

        public override string Description =>
            IModIconModifier.SimpleFormatDescription(
                ModDescriptionsContainer.Instance.reflection.Value, Counter.Count);
        
        public override string SubInfo => Counter.SubInfo;
    }
}