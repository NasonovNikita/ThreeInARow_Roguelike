using System.Collections.Generic;
using System.Linq;
using Battle.Items;
using Battle.Match3;
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
        public UnitHp Hp;
        public Mana mana;

        public int manaPerGem;

        public UnitDamage unitDamage;

        [SerializeField] protected internal DmgType chosenElement;

        private StateAnimationController stateAnimationController;

        public List<Modifier> allMods = new();

        private Grid grid;

        public List<Item> items;

        public List<Spell> spells;

        public bool IsBurning => allMods.Exists(v => v.type is ModType.Burning);

        protected BattleManager manager;

        private bool IsMissingOnFreeze =>
           Random.Range(1, 101) <= (allMods.Exists(v => v.type is ModType.Freezing) ? 40 : 0);

        public bool canFullyFreeze;

        public void Update()
        {
            if (!allMods.Exists(mod => mod.type == ModType.Burning && mod.Use() != 0)) StopBurning();
            if (!allMods.Exists(mod => mod.type == ModType.Poisoning && mod.Use() != 0)) StopPoisoning();
            if (!allMods.Exists(mod => mod.type == ModType.Freezing && mod.Use() != 0)) StopFreezing();
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
            grid = FindFirstObjectByType<Grid>();
            mana.Refill(CountMana());
            Damage dmg = unitDamage.GetGemsDamage(grid.destroyed);
            
            if (!dmg.IsZero() && !IsMissingOnFreeze)
            {
                target.DoDamage(dmg);
                switch (this)
                {
                    case Enemy:
                        EToPDamageLog.Log((Enemy) this, (Player) target, dmg);
                        break;
                    case Player:
                        PToEDamageLog.Log((Enemy) target, (Player) this, dmg);
                        break;
                }
                UseElementOnDestroyed(grid.destroyed, target);
            }
            
            grid.ClearDestroyed();
        }

        public virtual void DoDamage(Damage dmg)
        {

            GotDamageLog.Log(this, Hp.DoDamage(dmg));

            if (Hp == 0)
            {
                NoHp();
            }
        }
        public void Delete()
        {
            DestroyImmediate(gameObject);
        }

        public bool Stunned()
        {
            return allMods.Exists(mod => mod.type == ModType.Stun && mod.Use() != 0);
        }

        private void UseElementOnDestroyed(IReadOnlyDictionary<GemType, int> destroyed, Unit target)
        {
            switch (chosenElement)
            {
                case DmgType.Light when destroyed[GemType.Yellow] != 0:
                    Hp.Heal(5 * destroyed[GemType.Yellow]);
                    break;
                case DmgType.Fire when destroyed[GemType.Red] != 0:
                    target.StartBurning(1);
                    break;
                case DmgType.Cold when destroyed[GemType.Blue] != 0:
                    if (canFullyFreeze && Tools.RandomChance(50))
                    {
                        target.Stun(1);
                        target.AddMod(new Modifier(1, ModType.Frozen));
                    }
                    else target.StartFreezing(1);
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
                onMove: () => { DoDamage(new Damage(fDmg: 10)); },
                delay: true)
            );
            AddHpMod(new Modifier(moves + 1, ModType.Mul,
                ModClass.DamageBase, isPositive: false, value: 0.25f));
            stateAnimationController.AddState(UnitStates.Burning);
        }

        public void StartPoisoning(int moves)
        {
            allMods.Add(new Modifier(
                moves,
                ModType.Poisoning,
                isPositive: false,
                onMove: () => { DoDamage(new Damage(cDmg: 15)); },
                delay: true)
            );
            try
            {
                allMods.First(v => v.IsPositive).TurnOff();
            }
            catch
            {
                // ignored
            }

            stateAnimationController.AddState(UnitStates.Poisoning);
        }

        public void StartFreezing(int moves)
        {
            allMods.Add(new Modifier(moves, ModType.Freezing, isPositive: false));
            stateAnimationController.AddState(UnitStates.Freezing);
            
            AddDamageMod(new Modifier(moves, ModType.Mul,
                ModClass.DamageBase, isPositive: false, value: -0.25f));
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
            Hp.AddMod(mod);
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