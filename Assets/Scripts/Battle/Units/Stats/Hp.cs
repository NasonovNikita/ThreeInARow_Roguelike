using System;
using Battle.Modifiers;
using UnityEngine;

namespace Battle.Units.Stats
{
    [Serializable]
    public class Hp : Stat
    {
        [SerializeField] public ModifierList onHealingMods = new();
        [SerializeField] public ModifierList onTakingDamageMods = new();

        public Hp(int value, int borderUp) : base(value, borderUp)
        {
        }

        public Hp(int v, Stat stat) : base(v, stat)
        {
        }

        public Hp(Stat stat) : base(stat)
        {
        }

        public Hp(int v) : base(v)
        {
        }

        public void Heal(int val)
        {
            val = Math.Max(0, IIntModifier.UseModList(onHealingMods.List, val));

            ChangeValue(val);
        }

        public void TakeDamage(int val)
        {
            val = Math.Max(0, IIntModifier.UseModList(onTakingDamageMods.List, val));

            ChangeValue(-val);
        }

        public Hp Save()
        {
            onHealingMods.RemoveTempModsAndUnAttach();
            onTakingDamageMods.RemoveTempModsAndUnAttach();
            UnAttach();

            return this;
        }

        public void Init()
        {
            onHealingMods.InitMods();
            onTakingDamageMods.InitMods();
        }
    }
}