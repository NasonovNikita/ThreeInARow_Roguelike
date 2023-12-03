using System;
using System.Collections.Generic;
using System.Linq;
using Battle.Items;
using Battle.Modifiers;
using Battle.Spells;
using Other;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Battle.Units
{
    public abstract class Unit : MonoBehaviour
    {
        public Stat hp;
        public Stat mana;

        public int manaPerGem;

        public Stat fDmg;
        public Stat cDmg;
        public Stat pDmg;
        public Stat lDmg;
        public Stat phDmg;
        public Stat mDmg;

        
        [NonSerialized] public Stat burnChance = new (10);
        [NonSerialized] public Stat poisonChance = new (10);
        [NonSerialized] public Stat freezeChance = new (10);

        private StateAnimationController stateAnimationController;

        protected Dictionary<DmgType, Stat> damage;

        public List<Modifier> stateModifiers = new();

        public List<Item> items;

        public List<Spell> spells;

        protected BattleManager manager;

        public void Update()
        {
            if (!stateModifiers.Exists(mod => mod.type == ModType.Burning && mod.Use() != 0)) StopBurning();
            if (!stateModifiers.Exists(mod => mod.type == ModType.Poisoning && mod.Use() != 0)) StopPoisoning();
            if (!stateModifiers.Exists(mod => mod.type == ModType.Frozen && mod.Use() != 0)) UnFreeze();
        }

        protected void TurnOn()
        {
            manager = FindFirstObjectByType<BattleManager>();

            stateAnimationController = GetComponentInChildren<StateAnimationController>();
        
            hp.Init();
            mana.Init();
            damage = new Dictionary<DmgType, Stat>
            {
                { DmgType.Fire, fDmg },
                { DmgType.Cold, cDmg },
                { DmgType.Poison, pDmg },
                { DmgType.Light, lDmg },
                { DmgType.Physic, phDmg },
                { DmgType.Magic, mDmg }
            };
            foreach (Stat stat in damage.Values)
            {
                stat.Init();
            }
        
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
    

        public virtual void DoDamage(Damage dmg)
        {
            int value = dmg.Get().Values.Sum();
            if (dmg.Get()[DmgType.Fire] != 0 && burnChance >= Random.Range(0, 101))
            {
                StartBurning(1);
            }
            if (dmg.Get()[DmgType.Poison] != 0 && poisonChance >= Random.Range(0, 101))
            {
                StartPoisoning(1);
            }
            if (dmg.Get()[DmgType.Cold] != 0 && freezeChance >= Random.Range(0, 101))
            {
                Freeze(1);
            }
            
            hp -= value;

            if (hp == 0)
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
            return stateModifiers.Exists(mod => mod.type == ModType.Stun && mod.Use() != 0);
        }

        public void StartBurning(int moves)
        {
            stateModifiers.Add(new Modifier(
                moves,
                ModType.Burning,
                onMove: () => { DoDamage(new Damage(10)); },
                delay: true)
            );
            stateAnimationController.AddState(UnitStates.Burning);
        }

        public void StartPoisoning(int moves)
        {
            stateModifiers.Add(new Modifier(
                moves,
                ModType.Poisoning,
                onMove: () => { DoDamage(new Damage(cDmg: 15)); },
                delay: true)
            );
            stateAnimationController.AddState(UnitStates.Poisoning);
        }

        public void Freeze(int moves)
        {
            stateModifiers.Add(new Modifier(
                moves,
                ModType.Frozen,
                onMove: () => { stateModifiers.Add(new Modifier(moves, ModType.Stun)); },
                delay: true)
            );
            stateAnimationController.AddState(UnitStates.Frozen);
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
            stateAnimationController.DeleteState(UnitStates.Frozen);
        }

        public abstract void Act();

        protected abstract void NoHp();
    }

    public enum UnitStates
    {
        Burning,
        Poisoning,
        Frozen
    }
}