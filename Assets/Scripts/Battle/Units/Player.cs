using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class Player : Unit
{
    public static PlayerData data;
    
    public int manaPerGem;

    private Grid grid;

    public new void TurnOn()
    {
        base.TurnOn();
        grid = FindFirstObjectByType<Grid>();
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
        manaPerGem = data.manaPerGem;
        hp = data.hp;
        mana = data.mana;
        damage = data.damage;
        statusModifiers = data.statusModifiers;
        items = data.items;
        spells = data.spells;
    }

    public void Save()
    {
        data.manaPerGem = manaPerGem;
        data.hp = hp;
        data.mana = mana;
        data.damage = damage;
        data.statusModifiers = statusModifiers;
        data.items = items;
        data.spells = spells;
    }
}