using System;
using System.Collections.Generic;
using Battle.Units.Modifiers;
using IModifier =  Battle.Units.Modifiers.IModifier;
using IStatModifier = Battle.Units.Modifiers.StatModifiers.IStatModifier;

namespace Battle.Units.Stats
{
    [Serializable]
    public class Damage : Stat
    {
        private List<IStatModifier> mods = new ();

        public Damage(int value, int borderUp, int borderDown = 0) : base(value, borderUp, borderDown) {}
        public Damage(int v, Stat stat) : base(v, stat) {}
        public Damage(Stat stat) : base(stat) {}
        public Damage(int v) : base(v) {}

        public int ApplyDamage(int val) => val * IStatModifier.UseModList(mods, value);

        public void AddMod(IStatModifier mod)
        {
            IModifier.AddModToList(mods, mod);
            AddModToGrid(mod);
        }

        public Damage Save()
        {
            mods = IModifier.CleanedModifiers(mods);

            return this;
        }
    }
}