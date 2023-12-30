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
        private List<Modifier> mods = new();

        [SerializeField] private Stat phDmg;
        [SerializeField] private Stat fDmg;
        [SerializeField] private Stat cDmg;
        [SerializeField] private Stat pDmg;
        [SerializeField] private Stat lDmg;
        [SerializeField] private Stat mDmg;

        public Damage GetGemsDamage(Dictionary<GemType, int> gems)
        {
            Damage dmg = new Damage(
                UseDamageMods<DamageMod>(UseDamageMods<TypedDamageMod>((int)phDmg * gems
                    .Sum(t => t.Key != GemType.Mana ? t.Value : 0))),
                GetGemDamage(gems, fDmg, GemType.Red),
                GetGemDamage(gems, cDmg, GemType.Blue),
                GetGemDamage(gems, pDmg, GemType.Green),
                GetGemDamage(gems, lDmg, GemType.Yellow)
            );

            return dmg;
        }

        private int GetGemDamage(IReadOnlyDictionary<GemType, int> gems, Stat dmg, GemType type) =>
            Math.Max(0, UseDamageMods<DamageMod>(UseDamageMods<TypedDamageMod>((int)dmg * gems[type], DmgType.Fire)));

        public void AddMod(Modifier mod)
        {
            mods.Add(mod);
        }

        private int UseDamageMods<T>(int val, DmgType type = DmgType.Physic)
        {
            var where = mods.Where(v =>
                    v.GetType() is T && (!(typeof(T) == typeof(TypedDamageMod)) || ((TypedDamageMod)v).dmgType == type))
                .ToList();
            float mulVal = 1 + where.Where(v => v.type == ModType.Mul).Sum(v => v.Use());
            int addVal = (int)where.Where(v => v.type == ModType.Add).Sum(v => v.Use());
            return (int)(val * mulVal) + addVal;
        }
    }

    public class DamageMod : Modifier
    {
        public DamageMod(int moves, ModType type, bool isPositive = true, float value = 1,
            Action onMove = null, bool delay = false) : base(moves, type,  isPositive, value, onMove, delay)
        {
        }
    }

    public class TypedDamageMod : Modifier
    {
        public readonly DmgType dmgType;
        
        public TypedDamageMod(int moves, ModType type, DmgType dmgType,  bool isPositive = true, float value = 1,
            Action onMove = null, bool delay = false) : base(moves, type,  isPositive, value, onMove, delay)
        {
            this.dmgType = dmgType;
        }
    }
}