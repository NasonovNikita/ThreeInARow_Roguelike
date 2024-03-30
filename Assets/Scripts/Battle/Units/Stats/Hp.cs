using System;
using System.Collections.Generic;
using Battle.Modifiers;
using Battle.Modifiers.StatModifiers;
using UI.Battle;
using UnityEngine;

namespace Battle.Units.Stats
{
    [Serializable]
    public class Hp : Stat
    {
        [SerializeField] private bool instakillProtected;
        [SerializeField] private int instakillProtectedPart;
        
        [SerializeField] [SerializeReference] private List<IStatModifier> onTakingDamageMods = new ();
        [SerializeField] [SerializeReference] private List<IStatModifier> onHealingMods = new ();

        public Hp(int value, int borderUp, int borderDown = 0) : base(value, borderUp, borderDown) {}
        public Hp(int v, Stat stat) : base(v, stat) {}
        public Hp(Stat stat) : base(stat) {}
        public Hp(int v) : base(v) {}

        public Action onHpChanged;

        public int Heal(int val)
        {
            val = Math.Max(0, IStatModifier.UseModList(onHealingMods, val));
            val = Math.Min(borderUp - value, val);
            value += val;
            
            onHpChanged?.Invoke();
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
            
            onHpChanged?.Invoke();
            hud.CreateHUD(val.ToString(), Color.red, Direction.Down);

            return val;
        }

        public void AddDamageMod(IStatModifier mod)
        {
            IConcatAble.AddToList(onTakingDamageMods, mod);
            if (onTakingDamageMods.Contains(mod)) AddModToGrid(mod);
        }

        public void AddHealingMod(IStatModifier mod)
        {
            onHealingMods.Add(mod);
            if (onHealingMods.Contains(mod)) AddModToGrid(mod);
        }

        public Hp Save()
        {
            onHealingMods = ISaveAble.SaveList(onHealingMods);
            onTakingDamageMods = ISaveAble.SaveList(onTakingDamageMods);

            return this;
        }
    }
}