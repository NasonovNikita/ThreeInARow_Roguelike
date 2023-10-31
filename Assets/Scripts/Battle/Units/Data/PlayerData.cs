using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "UnitData/PlayerData")]
[Serializable]
public class PlayerData : UnitData
{
    [SerializeField]
    public int money;


    public PlayerData(int manaPerGem, Stat hp, Stat mana, Stat fDmg, Stat cDmg, Stat pDmg, Stat lDmg, Stat phDmg, List<Modifier> statusModifiers, List<Item> items,
        List<Spell> spells, int money) : base(hp, mana, fDmg, cDmg, pDmg, lDmg, phDmg, statusModifiers, items, spells)
    {
        this.manaPerGem = manaPerGem;
        this.money = money;
    }

    
    public PlayerData() : base()
    {
        manaPerGem = 20;
        money = 0;
    }
}