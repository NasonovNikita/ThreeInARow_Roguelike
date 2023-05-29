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

    public List<Item> items;

    public List<Spell> spells;

    protected BattleManager manager;

    protected void TurnOn()
    {
        manager = FindFirstObjectByType<BattleManager>();
        hp.Init();
        mana.Init();
        damage.Init();

        foreach (Item item in items)
        {
            item.Use(this);
        }
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
        return statusModifiers.Exists(mod => mod.type == ModType.Stun && mod.Use() != 0);
    }

    public abstract void Act();

    protected abstract void NoHp();
}