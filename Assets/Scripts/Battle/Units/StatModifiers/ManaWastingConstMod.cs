using System;
using Battle.Modifiers;

namespace Battle.Units.StatModifiers
{
    [Serializable]
    public class ManaWastingConstMod : ManaWastingMod
    {
        public ManaWastingConstMod(int value, bool save = false) : base(value, save)
        {
        }

        protected override bool CanConcat(Modifier other)
        {
            return other is ManaWastingConstMod;
        }
    }
}