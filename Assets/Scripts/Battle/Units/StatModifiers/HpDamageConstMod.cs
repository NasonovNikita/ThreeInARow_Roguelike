using Battle.Modifiers;

namespace Battle.Units.StatModifiers
{
    public class HpDamageConstMod : HpDamageMod
    {
        public HpDamageConstMod(int value, bool isSaved = false) : base(value, isSaved)
        {
        }

        protected override bool HiddenCanConcat(Modifier other) =>
            other is HpDamageConstMod;
    }
}