using System.Collections.Generic;
using System.Linq;
using Battle;
using Battle.Modifiers;
using Battle.Units;
using Other;
using UnityEngine;

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

    private StateAnimationController stateAnimationController;

    protected Dictionary<DmgType, Stat> damage;

    public List<Modifier> stateModifiers = new();

    public List<Item> items;

    public List<Spell> spells;

    protected BattleManager manager;

    public void Update()
    {
        if (!stateModifiers.Exists(mod => mod.type == ModType.Burning && mod.Use() != 0)) StopBurning();
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
            { DmgType.Physic, phDmg }
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

    public void StopBurning()
    {
        stateAnimationController.DeleteState(UnitStates.Burning);
    }

    public abstract void Act();

    protected abstract void NoHp();
}

public enum UnitStates
{
    Burning
}