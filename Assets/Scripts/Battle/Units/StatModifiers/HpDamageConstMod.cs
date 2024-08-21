using Battle.Modifiers;

namespace Battle.Units.StatModifiers
{
    public class HpDamageConstMod : HpDamageMod
    {
        public HpDamageConstMod(int value, bool save = false) : base(value, save)
        {
        }

        protected override bool HiddenCanConcat(Modifier other) =>
            other is HpDamageConstMod;
    }
}