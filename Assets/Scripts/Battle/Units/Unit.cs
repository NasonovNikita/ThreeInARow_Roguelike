using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField]
    public Stat hp;
    [SerializeField]
    public Stat mana;

    [SerializeField]
    public Stat damage;

    public List<Modifier> statusModifiers = new();

    protected BattleManager manager;

    public void Awake()
    {
        manager = FindObjectOfType<BattleManager>();
        hp.Init();
        mana.Init();
        damage.Init();
    }

    public virtual void DoDamage(int value)
    {
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
        return statusModifiers.Exists(mod => mod.type == ModType.Stun);
    }

    public abstract void Act();

    protected abstract void NoHp();
}