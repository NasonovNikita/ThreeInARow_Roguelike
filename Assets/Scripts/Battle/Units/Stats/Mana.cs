using System;
using Battle.Modifiers;
using UnityEngine;

namespace Battle.Units.Stats
{
    [Serializable]
    public class Mana : Stat
    {
        [SerializeField] public ModifierList refillingMods = new();
        [SerializeField] public ModifierList wastingMods = new();

        public Mana(int value, int borderUp) : base(value, borderUp)
        {
        }

        public Mana(int v, Stat stat) : base(v, stat)
        {
        }

        public Mana(Stat stat) : base(stat)
        {
        }

        public Mana(int v) : base(v)
        {
        }

        public void Refill(int val)
        {
            val = Math.Max(0, IIntModifier.UseModList(refillingMods.ModList, val));

            ChangeValue(val);
        }

        public void Waste(int val)
        {
            val = Math.Max(0, IIntModifier.UseModList(wastingMods.ModList, val));

            ChangeValue(-val);
        }

        public Mana Save()
        {
            refillingMods.SaveMods();
            wastingMods.SaveMods();
            UnAttach();

            return this;
        }

        public void Init()
        {
            refillingMods.InitMods();
            wastingMods.InitMods();
        }
    }
}