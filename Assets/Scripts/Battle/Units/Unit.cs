using System.Collections.Generic;
using System.Linq;
using Battle;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public Stat hp;
    public Stat mana;

    public Stat fDmg;
    public Stat cDmg;
    public Stat pDmg;
    public Stat lDmg;
    public Stat phDmg;

    protected Dictionary<DmgType, Stat> damage;

    public List<Modifier> statusModifiers = new();

    public List<Item> items;

    public List<Spell> spells;

    protected BattleManager manager;

    protected void TurnOn()
    {
        manager = FindFirstObjectByType<BattleManager>();
        
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

        foreach (Item item in items)
        {
            item.Use(this);
        }

        foreach (Spell spell in spells)
        {
            spell.Init();
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
        return statusModifiers.Exists(mod => mod.type == ModType.Stun && mod.Use() != 0);
    }

    public abstract void Act();

    protected abstract void NoHp();
}