using System;
using System.Collections.Generic;
using UI.Battle;
using UnityEngine;
using IModifier = Battle.Units.Modifiers.IModifier;
using IStatModifier =  Battle.Units.Modifiers.StatModifiers.IStatModifier;

namespace Battle.Units.Stats
{
    [Serializable]
    public class Mana : Stat
    {
        private List<IStatModifier> wastingMods = new();
        private List<IStatModifier> refillingMods = new();

        public Mana(int value, int borderUp, int borderDown = 0) : base(value, borderUp, borderDown) {}
        public Mana(int v, Stat stat) : base(v, stat) {}
        public Mana(Stat stat) : base(stat) {}
        public Mana(int v) : base(v) {}

        public int Refill(int val)
        {
            val = Math.Max(0, IStatModifier.UseModList(refillingMods, val));
            val = Math.Min(borderUp - value, val);
            value += val;
            
            hud.CreateHUD(val.ToString(), Color.magenta, Direction.Up);
            
            return val;
        }

        public int Waste(int val)
        {
            val = Math.Max(0, IStatModifier.UseModList(wastingMods, val));
            val = Math.Min(value, val);
            value -= val;
            
            hud.CreateHUD(val.ToString(), Color.magenta, Direction.Up);
            
            return val;
        }

        public void AddWastingMod(IStatModifier mod)
        {
            IModifier.AddModToList(wastingMods, (IModifier)mod);
            AddModToGrid(mod);
        }

        public void AddRefillingMod(IStatModifier mod) => IModifier.AddModToList(refillingMods, mod);

        public Mana Save()
        {
            refillingMods = IModifier.CleanedModifiers(refillingMods);
            wastingMods = IModifier.CleanedModifiers(wastingMods);

            return this;
        }
    }
}