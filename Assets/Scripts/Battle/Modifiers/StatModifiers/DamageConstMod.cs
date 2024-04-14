using System;

namespace Battle.Modifiers.StatModifiers
{
    [Serializable]
    public class DamageConstMod : DamageMod
    {
        public DamageConstMod(int value, bool save = false) : base(value, save) {}
        
        protected override bool CanConcat(Modifier other) => other is DamageConstMod;
    }
}
