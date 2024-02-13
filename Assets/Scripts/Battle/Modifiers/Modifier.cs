using System;
using Core;
using Knot.Localization;
using UnityEngine;
using UnityEngine.Serialization;

namespace Battle.Modifiers
{
    [Serializable]
    public class Modifier
    {
        public int moves;

        public ModType type;
        public ModClass workPattern;
        public DmgType dmgType;
        public bool always;
    
        public float value;

        [FormerlySerializedAs("IsPositive")] public bool isPositive;

        public Sprite Sprite
        {
            get
            {
                return workPattern switch
                {
                    ModClass.DamageBase => SpritesContainer.instance.damageMod,
                    ModClass.DamageTyped => SpritesContainer.instance.damageMod,
                    ModClass.DamageTypedStat => SpritesContainer.instance.damageMod,
                    ModClass.HpDamageBase when isPositive => SpritesContainer.instance.shield,
                    ModClass.HpDamageBase when  !isPositive => SpritesContainer.instance.shieldBroken,
                    ModClass.HpDamageTyped when isPositive => SpritesContainer.instance.shield,
                    ModClass.HpDamageTyped when  !isPositive => SpritesContainer.instance.shieldBroken,
                    ModClass.HpHealing => SpritesContainer.instance.hpHealing,
                    ModClass.ManaRefill => SpritesContainer.instance.manaMod,
                    ModClass.ManaWaste => SpritesContainer.instance.manaMod,
                    ModClass.Standard => type switch
                    {
                        ModType.Stun => SpritesContainer.instance.stun,
                        ModType.Freezing => null,
                        ModType.Burning => null,
                        ModType.Poisoning => null,
                        ModType.Frozen => SpritesContainer.instance.frozen,
                        ModType.Blind => SpritesContainer.instance.blind,
                        ModType.Irritated => SpritesContainer.instance.irritation,
                        ModType.Ignition => SpritesContainer.instance.ignition,
                        ModType.Add => throw new Exception("Non standard type found on standard mod"),
                        ModType.Mul => throw new Exception("Non standard type found on standard mod"),
                        _ => throw new ArgumentOutOfRangeException()
                    },
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        private string DmgTypeString => LocalizedStringsKeys.instance.DmgTypes(dmgType);

        public string Description
        {
            get
            {
                string moreLess = value > 0
                    ? LocalizedStringsKeys.instance.more.Value
                    : LocalizedStringsKeys.instance.less.Value;
                string changeInfo;
                switch (type)
                {
                    case ModType.Blind:
                        return LocalizedStringsKeys.instance.blind.Value;
                    case ModType.Stun:
                        return LocalizedStringsKeys.instance.stun.Value;
                    case ModType.Freezing:
                        return string.Format(LocalizedStringsKeys.instance.freezing.Value,
                            ElementsProperties.MissingOnFreezeChance);
                    case ModType.Burning:
                        return string.Format(LocalizedStringsKeys.instance.burning.Value,
                            ElementsProperties.FireDamage);
                    case ModType.Poisoning:
                        return string.Format(LocalizedStringsKeys.instance.poisoning.Value,
                            ElementsProperties.PoisonDamage);
                    case ModType.Frozen:
                        return LocalizedStringsKeys.instance.frozen.Value;
                    case ModType.Irritated:
                        return string.Format(LocalizedStringsKeys.instance.irritated.Value, (int) value);
                    case ModType.Ignition:
                        return LocalizedStringsKeys.instance.ignition.Value;
                    case ModType.Add:
                        changeInfo = $"{Math.Abs(Use)} {moreLess}";
                        break;
                    case ModType.Mul:
                        changeInfo = $"{(int) Math.Abs(Use * 100)}% {moreLess}";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                return workPattern switch
                {
                    ModClass.DamageBase => string.Format(LocalizedStringsKeys.instance.dealDmg.Value,
                        changeInfo),
                    ModClass.DamageTyped => string.Format(LocalizedStringsKeys.instance.dealDmg.Value,
                        $"{changeInfo} {DmgTypeString}"),
                    ModClass.DamageTypedStat => string.Format(LocalizedStringsKeys.instance.dealDmgPerGem.Value,
                        $"{changeInfo} {DmgTypeString}"),
                    ModClass.HpDamageBase => string.Format(LocalizedStringsKeys.instance.getDmg.Value,
                        changeInfo),
                    ModClass.HpDamageTyped => string.Format(LocalizedStringsKeys.instance.getDmg.Value,
                        $"{changeInfo} {DmgTypeString}"),
                    ModClass.HpHealing => string.Format(LocalizedStringsKeys.instance.healHp.Value,
                        changeInfo),
                    ModClass.ManaRefill => string.Format(LocalizedStringsKeys.instance.refillMana.Value,
                        changeInfo),
                    ModClass.ManaWaste => string.Format(LocalizedStringsKeys.instance.wasteMana.Value,
                        changeInfo),
                    ModClass.Standard => 
                        throw new Exception("can't be standard because no standard type was caught"),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        [SerializeField] public bool delay;

        private Action onMove;

        public Modifier(int moves, ModType type, ModClass workPattern = ModClass.Standard,
            DmgType dmgType = DmgType.Physic, bool isPositive = true, float value = 1, Action onMove = null,
            bool delay = false, bool always = false)
        {
            this.moves = moves;
            this.type = type;
            this.workPattern = workPattern;
            this.dmgType = dmgType;
            this.value = value;
            this.onMove = onMove;
            this.delay = delay;
            this.always = always;
            this.isPositive = isPositive;
            Log.OnLog += Move;
        }

        public float Use => moves != 0 ? value : 0;

        public void TurnOff()
        {
            moves = 0;
        }
        
        private void Move(Log log)
        {
            if (log is BattleEndLog && !always)
            {
                TurnOff();
                return;
            } 
            if (log is not TurnLog) return;
            
            if (delay)
            {
                delay = false;
                return;
            }
            if (moves == 0) return;
            onMove?.Invoke();
            if (moves <= 0) return;
            moves -= 1;
        }
    }

    public enum ModClass
    {
        DamageBase,
        DamageTyped,
        DamageTypedStat,
        HpDamageBase,
        HpDamageTyped,
        HpHealing,
        ManaRefill,
        ManaWaste,
        Standard
    }
}