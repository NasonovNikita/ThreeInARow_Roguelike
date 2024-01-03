using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Match3;
using Battle.Modifiers;
using UnityEngine;

namespace Battle.Units.Stats
{
    [Serializable]
    public class UnitDamage
    {
        [SerializeField] private List<Modifier> mods;

        [SerializeField] public Stat phDmg;
        [SerializeField] public Stat fDmg;
        [SerializeField] public Stat cDmg;
        [SerializeField] public Stat pDmg;
        [SerializeField] public Stat lDmg;
        [SerializeField] public Stat mDmg;

        public Damage GetGemsDamage(Dictionary<GemType, int> gems)
        {
            Damage dmg = new Damage(
                UseDamageMods(
                    UseDamageMods(
                        UseDamageMods((int)phDmg, 
                            ModClass.DamageTypedStat) * gems.Sum(t => t.Key != GemType.Mana ? t.Value : 0),
                        ModClass.DamageTyped),
                    ModClass.DamageBase),
                GetGemDamage(gems, fDmg, GemType.Red, DmgType.Fire),
                GetGemDamage(gems, cDmg, GemType.Blue, DmgType.Cold),
                GetGemDamage(gems, pDmg, GemType.Green, DmgType.Poison),
                GetGemDamage(gems, lDmg, GemType.Yellow, DmgType.Light)
            );

            return dmg;
        }

        private int GetGemDamage(IReadOnlyDictionary<GemType, int> gems, Stat dmg, GemType type, DmgType dmgType) =>
            Math.Max(0,
                UseDamageMods(
                    UseDamageMods(
                        UseDamageMods((int)dmg, 
                            ModClass.DamageTypedStat, dmgType) * gems[type],
                        ModClass.DamageTyped, dmgType),
                    ModClass.DamageBase)
                );

        public void AddMod(Modifier mod)
        {
            mods.Add(mod);
        }

        private int UseDamageMods(int val, ModClass workPattern, DmgType dmgType = DmgType.Physic)
        {
            var where = mods.Where(v => v.workPattern == workPattern && v.dmgType == dmgType).ToList();
            float mulVal = 1 + where.Where(v => v.type == ModType.Mul).Sum(v => v.Use());
            int addVal = (int)where.Where(v => v.type == ModType.Add).Sum(v => v.Use());
            return (int)(val * mulVal) + addVal;
        }
    }
}