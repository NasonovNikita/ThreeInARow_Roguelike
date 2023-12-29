using System;
using System.Collections.Generic;
using System.Linq;
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
                UseDamageMods<DamageMod>(UseDamageMods<TypedDamageMod>((int) phDmg * gems
                    .Sum(t => t.Key != GemType.Mana ? t.Value : 0))),
                UseDamageMods<DamageMod>(UseDamageMods<TypedDamageMod>((int) fDmg * gems[GemType.Red], DmgType.Fire)),
                UseDamageMods<DamageMod>(UseDamageMods<TypedDamageMod>((int) cDmg * gems[GemType.Blue], DmgType.Cold)),
                UseDamageMods<DamageMod>(UseDamageMods<TypedDamageMod>((int) pDmg * gems[GemType.Green], DmgType.Poison)),
                UseDamageMods<DamageMod>(UseDamageMods<TypedDamageMod>((int) lDmg * gems[GemType.Yellow], DmgType.Light))
            );

            return dmg;
        }

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