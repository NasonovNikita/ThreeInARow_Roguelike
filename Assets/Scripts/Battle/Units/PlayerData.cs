using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "PlayerData")]
[Serializable]
public class PlayerData : ScriptableObject
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
    [SerializeField]
    public List<Item> items;


    public PlayerData(int manaPerGem, Stat hp, Stat mana, Stat damage, List<Modifier> statusModifiers, List<Item> items)
    {
        this.manaPerGem = manaPerGem;
        this.hp = hp;
        this.mana = mana;
        this.damage = damage;
        this.statusModifiers = statusModifiers;
        this.items = items;
    }

    public PlayerData()
    {
        manaPerGem = 20;
        hp = new Stat(200);
        mana = new Stat(100);
        damage = new Stat(20);
        statusModifiers = new List<Modifier>();
        items = new List<Item>();
    }
}