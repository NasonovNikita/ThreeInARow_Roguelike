using System.Collections.Generic;
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
    
    [SerializeField]
    protected GameObject box;
    
    [SerializeField]
    protected BattleManager manager;

    public List<Modifier> DamageModifiers;

    public void ChangeHp(int change)
    {
        hp += change;
        if (hp <= 0)
        {
            hp = 0;
        }
        else if (hp > baseHp)
        {
            hp = baseHp;
        }
    }

    protected void ChangeMana(int change)
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

    public void Delete()
    {
        Destroy(box.gameObject);
    }

    public abstract void Act();
}