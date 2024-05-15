namespace Battle.Modifiers.StatModifiers
{
    public class HpDamageConstMod : HpDamageMod
    {
        public HpDamageConstMod(int value, bool save = false) : base(value, save) {}

        protected override bool CanConcat(Modifier other) => other is HpDamageConstMod;
    }
}