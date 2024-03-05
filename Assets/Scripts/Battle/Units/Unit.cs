using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Battle.Items;
using Battle.Match3;
using Battle.Modifiers;
using Battle.Spells;
using Battle.Units.Stats;
using Core;
using Other;
using UI.Battle;
using UI.MessageWindows;
using UnityEngine;
using UnityEngine.Serialization;
using Grid = Battle.Match3.Grid;

namespace Battle.Units
{
    public abstract class Unit : MonoBehaviour
    {
        [FormerlySerializedAs("Hp")] public Hp hp;
        public Mana mana;

        public int manaPerGem;

        [FormerlySerializedAs("unitDamage")] public UnitDamage damage;

        [SerializeField] protected internal DmgType chosenElement;

        [SerializeField] private ModIconGrid modIconGrid;

        [SerializeField] private InfoObject unitInfo;

        [SerializeField] private bool instakillProtected;

        public const float WaitTime = 0.5f;
        
        private string Info =>
            LocalizedStringsKeys.instance.unitInfoLocalizedKey.Value
                .FormatByKeys(new Dictionary<string, string>
                {
                    {"{phDmg}", damage.phDmg.value.ToString()},
                    {"{fDmg}", damage.fDmg.value.ToString()},
                    {"{cDmg}", damage.cDmg.value.ToString()},
                    {"{pDmg}", damage.pDmg.value.ToString()},
                    {"{lDmg}", damage.lDmg.value.ToString()},
                    { "{mDmg}", damage.mDmg.value.ToString()},
                    {"{currentElement}", LocalizedStringsKeys.instance.DmgType(chosenElement)}
                })  
                .Split("\n")
                .Where(line =>
                    !line.Contains(" 0") &&
                    !line.Contains(LocalizedStringsKeys.instance.physic.Value))
                .Aggregate("",
                    (current,
                        line) => current + line + "\n")
                .Trim();

        public abstract Unit Target { get; }

        [SerializeField] private StateAnimationController stateAnimationController;

        public List<Modifier> allMods = new();

        private Grid grid;

        protected BattleManager manager;

        public List<Item> items;

        public List<Spell> spells;

        public bool IsBurning => allMods.Exists(v => v.type is ModType.Burning);

        public bool IsStunned =>
            allMods.Exists(mod => mod.type == ModType.Stun && mod.Use != 0);

        public bool IsBlind =>
            allMods.Exists(mod => mod.type == ModType.Blind && mod.Use != 0);

        private bool IsMissingOnFreeze =>
            Tools.Random.RandomChance(allMods.Exists(v => v.type is ModType.Freezing)
            ? ElementsProperties.MissingOnFreezeChance
            : 0);

        public bool canFullyFreeze;

        public void Update()
        {
            if (!allMods.Exists(mod => mod.type == ModType.Burning && mod.Use != 0)) StopBurning();
            if (!allMods.Exists(mod => mod.type == ModType.Poisoning && mod.Use != 0)) StopPoisoning();
            if (!allMods.Exists(mod => mod.type == ModType.Freezing && mod.Use != 0)) StopFreezing();
        }

        protected void TurnOn()
        {
            manager = FindFirstObjectByType<BattleManager>();
            grid = FindFirstObjectByType<Grid>();

            unitInfo.text = Info;

            Tools.InstantiateAll(items);

            if (modIconGrid == null) throw new Exception("Unit must have modIconGrid");
            ShowPermanentMods();

            foreach (Item item in items)
            {
                item.Use(this);
            }

            Tools.InstantiateAll(spells);
        
            foreach (Spell spell in spells)
            {
                spell.Init(this);
            }
        }
    
        public virtual IEnumerator Act()
        {
            grid = FindFirstObjectByType<Grid>();
            RefillMana(CountMana());
            Damage dmg = CountDamage;

            var missed = IsMissingOnFreeze;
            if (!dmg.IsZero && !missed)
            {
                Target.TakeDamage(dmg);
                UseElementOnDestroyed(grid.destroyed);
            }

            if (missed)
            {
                UnitHUD.CreateStringHud(Target, LocalizedStringsKeys.instance.miss.Value, true);
            }
            
            grid.ClearDestroyed();

            yield return new WaitForSeconds(WaitTime);
        }

        protected virtual Damage CountDamage => damage.GetGemsDamage(grid.destroyed);

        public virtual void TakeDamage(Damage dmg)
        {
            int gotDamage = hp.TakeDamage(dmg);
            GotDamageLog.Log(this, gotDamage);
            UnitHUD.CreateStatChangeHud(this, hp, -gotDamage);

            OnHpChanged(gotDamage);
        }

        private void OnHpChanged(int gotDamage)
        {
            if (hp > 0) return;
            
            if (gotDamage > hp.borderUp * 0.3f && instakillProtected)
            {
                hp.value = (int)(hp.borderUp * 0.3f);
                UnitHUD.CreateStringHud(this,
                    LocalizedStringsKeys.instance.instakillProtectionLocalizedKey.Value,
                    true);
                instakillProtected = false;
            }
            else
            {
                NoHp();
            }
        }

        public void Heal(int val)
        {
            int healed = hp.Heal(val);
            UnitHUD.CreateStatChangeHud(this, hp, healed);
        }

        public void WasteMana(int val)
        {
            int wasted = mana.Waste(val);
            UnitHUD.CreateStatChangeHud(this, mana, -wasted);
        }

        public void RefillMana(int val)
        {
            int refilled = mana.Refill(val);
            UnitHUD.CreateStatChangeHud(this, mana, refilled);
        }
        public void Delete()
        {
            DestroyImmediate(gameObject);
        }

        private void UseElementOnDestroyed(IReadOnlyDictionary<GemType, int> destroyed)
        {
            switch (chosenElement)
            {
                case DmgType.Light when destroyed[GemType.Yellow] != 0:
                    Heal(ElementsProperties.LightHealRate * destroyed[GemType.Yellow]);
                    break;
                case DmgType.Fire when destroyed[GemType.Red] != 0:
                    Target.StartBurning(1);
                    break;
                case DmgType.Cold when destroyed[GemType.Blue] != 0:
                    if (canFullyFreeze && Tools.Random.RandomChance(ElementsProperties.TotalFreezingChance))
                    {
                        Target.Stun(1);
                        Target.AddMod(new Modifier(1, ModType.Frozen));
                    }
                    else Target.StartFreezing(1);
                    break;
                case DmgType.Poison when destroyed[GemType.Green] != 0:
                    Target.StartPoisoning(1);
                    break;
                case DmgType.Physic:
                    break;
                case DmgType.Magic:
                    break;
            }
        }

        protected int CountMana()
        {
            return grid.destroyed[GemType.Mana] * manaPerGem;
        }

        public void Stun(int moves)
        {
            allMods.Add(new Modifier(moves, ModType.Stun, isPositive: false));
        }

        public void StartBurning(int moves)
        {
            allMods.Add(new Modifier(
                moves,
                ModType.Burning,
                isPositive: false,
                onMove: () => OnHpChanged(hp.Burn(ElementsProperties.FireDamage)),
                delay: true)
            );
            AddHpMod(new Modifier(moves + 1, ModType.Mul,
                ModClass.HpDamageBase, isPositive: false, value: ElementsProperties.FiredDamageModVal));
            stateAnimationController.AddState(UnitStates.Burning);
        }

        public void StartPoisoning(int moves)
        {
            allMods.Add(new Modifier(
                moves,
                ModType.Poisoning,
                isPositive: false,
                onMove: () => OnHpChanged(hp.Poison(ElementsProperties.PoisonDamage)),
                delay: true)
            );

            if (allMods.Exists(v => v.isPositive && !v.always))
                allMods.First(v => v.isPositive && !v.always).TurnOff();

            stateAnimationController.AddState(UnitStates.Poisoning);
        }

        public void StartFreezing(int moves)
        {
            allMods.Add(new Modifier(moves, ModType.Freezing, isPositive: false));
            stateAnimationController.AddState(UnitStates.Freezing);
            
            AddDamageMod(new Modifier(moves, ModType.Mul,
                ModClass.DamageBase, isPositive: false, value: -ElementsProperties.ColdDamageLoss));
        }

        public void StopBurning()
        {
            stateAnimationController.DeleteState(UnitStates.Burning);
        }

        public void StopPoisoning()
        {
            stateAnimationController.DeleteState(UnitStates.Poisoning);
        }

        public void StopFreezing()
        {
            stateAnimationController.DeleteState(UnitStates.Freezing);
        }

        public void AddHpMod(Modifier mod)
        {
            hp.AddMod(mod);
            AddMod(mod);
        }

        public void AddDamageMod(Modifier mod)
        {
            damage.AddMod(mod);
            AddMod(mod);
        }

        public void AddManaMod(Modifier mod)
        {
            mana.AddMod(mod);
            AddMod(mod);
        }

        public void AddMod(Modifier mod)
        {
            allMods.Add(mod);
            modIconGrid.Add(mod);
        }

        private void ShowPermanentMods()
        {
            foreach (Modifier mod in allMods)
            {
                modIconGrid.Add(mod);
            }
        }

        protected virtual void NoHp()
        {
            DeathLog.Log(this);
        }
    }

    public enum UnitStates
    {
        Burning,
        Poisoning,
        Freezing
    }
}