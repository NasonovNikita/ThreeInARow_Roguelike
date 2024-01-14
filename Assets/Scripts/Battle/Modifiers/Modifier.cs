using System;
using Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Battle.Modifiers
{
    [Serializable]
    public class Modifier
    {
        public static readonly int MissingOnFreezingChance = 40;
        
        public int moves;

        public ModType type;
        public ModClass workPattern;
        public DmgType dmgType;
        private bool always;
    
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
                    ModClass.HpDamageBase when  !isPositive => SpritesContainer.instance.empty,
                    ModClass.HpDamageTyped when isPositive => SpritesContainer.instance.shield,
                    ModClass.HpDamageTyped when  !isPositive => SpritesContainer.instance.empty,
                    ModClass.HpHealing => SpritesContainer.instance.empty,
                    ModClass.ManaRefill => SpritesContainer.instance.manaMod,
                    ModClass.ManaWaste => SpritesContainer.instance.manaMod,
                    ModClass.Standard => type switch
                    {
                        ModType.Stun => SpritesContainer.instance.empty,
                        ModType.Freezing => SpritesContainer.instance.empty,
                        ModType.Burning => SpritesContainer.instance.empty,
                        ModType.Poisoning => SpritesContainer.instance.empty,
                        ModType.Frozen => SpritesContainer.instance.empty,
                        ModType.Blind => SpritesContainer.instance.empty,
                        ModType.Irritated => SpritesContainer.instance.empty,
                        ModType.Ignition => SpritesContainer.instance.empty,
                        ModType.Add => throw new Exception("Non standard type found on standard mod"),
                        ModType.Mul => throw new Exception("Non standard type found on standard mod"),
                        _ => throw new ArgumentOutOfRangeException()
                    },
                    _ => throw new ArgumentOutOfRangeException()
                }; // TODO
            }
            
        }

        public string Description
        {
            get
            {
                string moreLess = value > 0 ? "more" : "less";
                string val;
                switch (type)
                {
                    case ModType.Blind:
                        return "Can't see the grid";
                    case ModType.Stun:
                        return "Skips turn";
                    case ModType.Freezing:
                        return $"Can miss with {MissingOnFreezingChance}% chance";
                    case ModType.Burning:
                        return "Takes 20 dmg every turn";
                    case ModType.Poisoning:
                        return "Takes 15 dmg every turn";
                    case ModType.Frozen:
                        return "Is stunned";
                    case ModType.Irritated:
                        return "Deals more damage if nobody dies this turn";
                    case ModType.Ignition:
                        return "Can get on fire randomly";
                    case ModType.Add:
                        val = $"{Math.Abs(Use)} {moreLess}";
                        break;
                    case ModType.Mul:
                        val = $"{(int) Math.Abs(Use * 100)}% {moreLess}";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                return workPattern switch
                {
                    ModClass.DamageBase => $"Deal {val} dmg",
                    ModClass.DamageTyped => $"Deal {val} {dmgType} dmg",
                    ModClass.DamageTypedStat => $"Deal {val} {dmgType} dmg per gem",
                    ModClass.HpDamageBase => $"Get {val} dmg",
                    ModClass.HpDamageTyped => $"Get {val} {dmgType} dmg",
                    ModClass.HpHealing => $"Heal {val} Hp",
                    ModClass.ManaRefill => $"Refill {val} mana",
                    ModClass.ManaWaste => $"waste {val} mana",
                    ModClass.Standard => 
                        throw new Exception("can't be standard because no standard type was caught"),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        [SerializeField] public bool delay;

        private Action onMove;

        public Modifier(int moves, ModType type, ModClass workPattern = ModClass.Standard, DmgType dmgType = DmgType.Physic, bool isPositive = true, float value = 1, Action onMove = null,
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