using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Items;
using Battle.Modifiers;
using Battle.Spells;
using Battle.Units.Stats;
using Other;
using UnityEngine;
using Grid = Battle.Match3.Grid;
using Random = UnityEngine.Random;

namespace Battle.Units
{
    public abstract class Unit : MonoBehaviour
    {
        public UnitHp unitHp;
        public Mana mana;

        public int manaPerGem;

        public UnitDamage unitDamage;

        [SerializeField] protected internal DmgType chosenElement;

        private StateAnimationController stateAnimationController;

        public List<Modifier> allMods = new();

        private Grid grid;

        public List<Item> items;

        public List<Spell> spells;

        protected BattleManager manager;

        protected bool IsMissingOnFreeze =>
            Random.Range(0, 101) <= (allMods.Exists(v => v.type is ModType.Freezing) ? 40 : 100);

        public void Update()
        {
            if (!allMods.Exists(mod => mod.type == ModType.Burning && mod.Use() != 0)) StopBurning();
            if (!allMods.Exists(mod => mod.type == ModType.Poisoning && mod.Use() != 0)) StopPoisoning();
            if (!allMods.Exists(mod => mod.type == ModType.Frozen && mod.Use() != 0)) UnFreeze();
        }

        protected void TurnOn()
        {
            manager = FindFirstObjectByType<BattleManager>();
            grid = FindFirstObjectByType<Grid>();

            stateAnimationController = GetComponentInChildren<StateAnimationController>();
        
            Tools.InstantiateAll(items);

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
    
        public void Act(Unit target)
        {
            mana.Refill(CountMana());
            Damage dmg = unitDamage.GetGemsDamage(grid.destroyed);
            
            if (dmg.IsZero() && IsMissingOnFreeze)
            {
                UseElementOnDestroyed(grid.destroyed, target);
                target.DoDamage(dmg);
            }
            
            grid.ClearDestroyed();
        }

        public virtual void DoDamage(Damage dmg)
        {

            unitHp.DoDamage(dmg);

            if (unitHp == 0)
            {
                NoHp();
            }
        }
        public void Delete()
        {
            Destroy(gameObject);
        }

        public bool Stunned()
        {
            return allMods.Exists(mod => mod.type == ModType.Stun && mod.Use() != 0);
        }

        public void UseElementOnDestroyed(Dictionary<GemType, int> destroyed, Unit target)
        {
            switch (chosenElement)
            {
                case DmgType.Light when destroyed[GemType.Yellow] != 0:
                    unitHp.Heal(5 * destroyed[GemType.Yellow]);
                    break;
                case DmgType.Fire when destroyed[GemType.Red] != 0:
                    target.StartBurning(1);
                    break;
                case DmgType.Cold when destroyed[GemType.Blue] != 0:
                    target.StartFreezing(1);
                    break;
                case DmgType.Poison when destroyed[GemType.Green] != 0:
                    target.StartPoisoning(1);
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

        public void StartBurning(int moves)
        {
            allMods.Add(new Modifier(
                moves,
                ModType.Burning,
                onMove: () => { DoDamage(new Damage(10)); },
                delay: true)
            );
            AddHpMod(new DamageMod(moves + 1, ModType.Mul, false, 0.25f));
            stateAnimationController.AddState(UnitStates.Burning);
        }

        public void StartPoisoning(int moves)
        {
            allMods.Add(new Modifier(
                moves,
                ModType.Poisoning,
                onMove: () => { DoDamage(new Damage(cDmg: 15)); },
                delay: true)
            );
            
            allMods.First(v => v.IsPositive).TurnOff();
            stateAnimationController.AddState(UnitStates.Poisoning);
        }

        public void StartFreezing(int moves)
        {
            allMods.Add(new Modifier(moves, ModType.Freezing));
            stateAnimationController.AddState(UnitStates.Freezing);
            // TODO missing chance 40% on freezing mod
            
            AddDamageMod(new DamageMod(moves, ModType.Mul, false, -0.25f));
        }

        public void StopBurning()
        {
            stateAnimationController.DeleteState(UnitStates.Burning);
        }

        public void StopPoisoning()
        {
            stateAnimationController.DeleteState(UnitStates.Poisoning);
        }

        public void UnFreeze()
        {
            stateAnimationController.DeleteState(UnitStates.Freezing);
        }

        public void AddHpMod(Modifier mod)
        {
            unitHp.AddMod(mod);
            allMods.Add(mod);
        }

        public void AddDamageMod(Modifier mod)
        {
            unitDamage.AddMod(mod);
            allMods.Add(mod);
        }

        public void AddManaMod(Modifier mod)
        {
            mana.AddMod(mod);
            allMods.Add(mod);
        }

        public void AddMod(Modifier mod)
        {
            allMods.Add(mod);
        }

        protected abstract void NoHp();
    }

    public enum UnitStates
    {
        Burning,
        Poisoning,
        Freezing
    }
}