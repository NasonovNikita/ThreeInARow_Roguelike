using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : Unit
{
    [SerializeField]
    private int manaPerGem;

    private Enemy _target;

    public Grid grid;

    public List<Enemy> enemies;
    

    private void Start()
    {
        SetTarget(enemies[0]);
    }
    
    private int Damage()
    {
        return grid.destroyed.Sum(type => type.Key != GemType.Mana ? type.Value : 0) * baseDamage;
    }

    private int CountMana()
    {
        return grid.destroyed.ContainsKey(GemType.Mana) ? grid.destroyed[GemType.Mana] * manaPerGem : 0;
    }

    public override void Act()
    {
        ChangeMana(CountMana());
        _target.ChangeHp(-Damage());
        grid.destroyed.Clear();
    }

    private void SetTarget(Enemy target)
    {
        _target = target;
    }
}