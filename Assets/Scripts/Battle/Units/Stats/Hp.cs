using System;
using System.Collections.Generic;
using Battle.Units.Modifiers;
using Battle.Units.Modifiers.StatModifiers;
using UI.Battle;
using UnityEngine;

namespace Battle.Units.Stats
{
    [Serializable]
    public class Hp : Stat
    {
        [SerializeField] private bool instakillProtected;
        [SerializeField] private int instakillProtectedPart;
        
        private List<IStatModifier> onTakingDamageMods = new ();
        private List<IStatModifier> onHealingMods = new ();

        public Hp(int value, int borderUp, int borderDown = 0) : base(value, borderUp, borderDown) {}
        public Hp(int v, Stat stat) : base(v, stat) {}
        public Hp(Stat stat) : base(stat) {}
        public Hp(int v) : base(v) {}

        public int Heal(int val)
        {
            val = Math.Max(0, IStatModifier.UseModList(onHealingMods, val));
            val = Math.Min(borderUp - value, val);
            value += val;
            
            hud.CreateHUD(val.ToString(), Color.green, Direction.Up);
            
            return val;
        }

        public int TakeDamage(int val)
        {
            val = Math.Max(0, IStatModifier.UseModList(onTakingDamageMods, val));
            val = Math.Min(value, val);
            
            if (instakillProtected && value >= instakillProtectedPart && val == value)
            {
                val = value - instakillProtectedPart;
                instakillProtected = false;
            }
            value -= val;
            
            hud.CreateHUD(val.ToString(), Color.red, Direction.Down);

            return val;
        }

        public void AddDamageMod(IStatModifier mod)
        {
            IModifier.AddModToList(onTakingDamageMods, mod);
            AddModToGrid(mod);
        }

        public void AddHealingMod(IStatModifier mod)
        {
            onHealingMods.Add(mod);
            AddModToGrid(mod);
        }

        public Hp Save()
        {
            onHealingMods = IModifier.CleanedModifiers(onHealingMods);
            onTakingDamageMods = IModifier.CleanedModifiers(onTakingDamageMods);

            return this;
        }
    }
}