using System;

namespace Battle.Modifiers.StatModifiers
{
    [Serializable]
    public class ManaWastingConstMod : ManaWastingMod
    {
        public ManaWastingConstMod(int value, bool save = false) : base(value, save) {}

        protected override bool CanConcat(Modifier other) => other is ManaWastingConstMod;
    }
}