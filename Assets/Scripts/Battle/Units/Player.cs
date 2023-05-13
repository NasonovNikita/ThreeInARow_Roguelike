using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class Player : Unit
{
    public static PlayerData data = new();
    
    public int manaPerGem;

    private Grid grid;

    public new void Awake()
    {
        base.Awake();
        grid = FindObjectOfType<Grid>();
    }

    private int CountMana()
    {
        return grid.destroyed.ContainsKey(GemType.Mana) ? grid.destroyed[GemType.Mana] * manaPerGem : 0;
    }

    private int CountDamage()
    {
        return (int) (grid.destroyed.Sum(type => type.Key != GemType.Mana ? type.Value : 0) * damage.GetValue());
    }

    public override void Act()
    {
        mana += CountMana();
        manager.target.DoDamage(CountDamage());
    }

    public override void DoDamage(int value)
    {
        base.DoDamage(value);
        
        if (value != 0)
        {
            AudioManager.instance.Play(AudioEnum.PlayerHit);
        }
    }

    protected override void NoHp()
    {
        StartCoroutine(manager.Die());
    }

    public void Load()
    {
        Debug.unityLogger.Log(data);
        manaPerGem = data.manaPerGem;
        hp = data.hp;
        mana = data.mana;
        damage = data.damage;
        statusModifiers = data.statusModifiers;
    }

    public void Save()
    {
        data.manaPerGem = manaPerGem;
        data.hp = hp;
        data.mana = mana;
        data.damage = damage;
        data.statusModifiers = statusModifiers;
    }
}

[Serializable]
public class PlayerData
{
    [SerializeField]
    public int manaPerGem;
    [SerializeField]
    public Stat hp;
    [SerializeField]
    public Stat mana;
    [SerializeField]
    public Stat damage;
    [SerializeField]
    public List<Modifier> statusModifiers;

    public PlayerData()
    {
        manaPerGem = 20;
        hp = new Stat(200);
        mana = new Stat(100);
        damage = new Stat(20);
        statusModifiers = new();
    }
}