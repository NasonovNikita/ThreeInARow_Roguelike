using System;
using Battle.Modifiers;

namespace Battle.Units.StatModifiers
{
    [Serializable]
    public class DamageConstMod : DamageMod
    {
        public DamageConstMod(int value, bool save = false) : base(value, save)
        {
        }

        protected override bool HiddenCanConcat(Modifier other)
        {
            return other is DamageConstMod;
        }
    }
}