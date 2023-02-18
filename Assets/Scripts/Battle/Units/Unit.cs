using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField]
    protected int hp;
    public int Hp => hp;
    
    [SerializeField]
    protected int baseHp;
    public int BaseHp => baseHp;

    [SerializeField]
    protected int mana;
    public int Mana => mana;
    
    [SerializeField]
    protected int baseMana;
    public int BaseMana => baseMana;

    [SerializeField]
    protected int baseDamage;
    public int BaseDamage => baseDamage;
    
    public BattleManager manager;

    public List<Modifier> DamageModifiers = new();

    public List<Modifier> StatusModifiers = new();

    public void ChangeHp(int change)
    {
        hp += change;
        if (hp <= 0)
        {
            hp = 0;
            NoHp();
        }
        else if (hp > baseHp)
        {
            hp = baseHp;
        }
    }

    public void ChangeMana(int change)
    {
        mana += change;
        if (mana <= 0)
        {
            mana = 0;
        }
        else if (mana > baseMana)
        {
            mana = baseMana;
        }
    }

    public void SetHp(int newHp)
    {
        hp = newHp;
    }

    public void SetMana(int newMana)
    {
        mana = newMana;
    }

    public void Delete()
    {
        Destroy(gameObject);
    }

    public bool Stunned()
    {
        return StatusModifiers.Exists(mod => mod.Type == ModifierType.Stun);
    }
    
    public void Move()
    {
        foreach (Modifier mod in DamageModifiers.ToList())
        {
            mod.Move();
            if (mod.Moves == 0)
            {
                DamageModifiers.Remove(mod);
            }
        }

        foreach (Modifier mod in StatusModifiers.ToList())
        {
            mod.Move();
            if (mod.Moves == 0)
            {
                StatusModifiers.Remove(mod);
            }
        }
    }

    public abstract void Act();

    protected abstract void NoHp();
}