using System;
using Battle.Modifiers;
using Battle.Modifiers.StatModifiers;
using UnityEngine;

namespace Battle.Units.Stats
{
    [Serializable]
    public class Hp : Stat
    {
        [SerializeField] public ModifierList<StatModifier> onTakingDamageMods = new ();
        [SerializeField] public ModifierList<StatModifier> onHealingMods = new ();

        public Hp(int value, int borderUp) : base(value, borderUp) {}
        public Hp(int v, Stat stat) : base(v, stat) {}
        public Hp(Stat stat) : base(stat) {}
        public Hp(int v) : base(v) {}

        public void Heal(int val)
        {
            val = Math.Max(0, StatModifier.UseModList(onHealingMods.ModList, val));
            
            ChangeValue(val);
        }

        public void TakeDamage(int val)
        {
            val = Math.Max(0, StatModifier.UseModList(onTakingDamageMods.ModList, val));
            
            ChangeValue(-val);
        }

        public Hp Save()
        {
            onHealingMods.SaveMods();
            onTakingDamageMods.SaveMods();
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