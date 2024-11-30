using System;
using Battle.Modifiers;

namespace Battle.Units.StatModifiers
{
    [Serializable]
    public class ManaWastingConstMod : ManaWastingMod
    {
        public ManaWastingConstMod(int value, bool isSaved = false) : base(value, isSaved)
        {
        }

        protected override bool HiddenCanConcat(Modifier other) =>
            other is ManaWastingConstMod;
    }
}