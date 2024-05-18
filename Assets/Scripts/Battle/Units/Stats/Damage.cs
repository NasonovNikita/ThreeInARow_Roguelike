using System;
using System.Linq;
using Battle.Modifiers;
using Battle.Modifiers.StatModifiers;
using UnityEngine;

namespace Battle.Units.Stats
{
    [Serializable]
    public class Damage : Stat
    {
        [SerializeField] public ModifierList mods = new ();

        public Damage(int value, int borderUp) : base(value, borderUp) {}
        public Damage(int v, Stat stat) : base(v, stat) {}
        public Damage(Stat stat) : base(stat) {}
        public Damage(int v) : base(v) {}

        public int ApplyDamage(int val) =>
            val * StatModifier.UseModList(mods.ModList, Value);

        public Damage Save()
        {
            mods.SaveMods();
            UnAttach();

            return this;
        }

        public void Init()
        {
            mods.InitMods();
        }
    }
}