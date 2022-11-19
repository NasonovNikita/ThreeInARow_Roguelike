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

    public List<Modifier> DamageModifiers = new();
    

    private void Start()
    {
        SetTarget(enemies[0]);
    }
    
    private int Damage()
    {
        float mulDamage = 1 + DamageModifiers.Sum(modifier => modifier.Type == ModifierType.DamageMul ? modifier.Value : 0);
        int addDamage = (int) DamageModifiers.Sum(modifier => modifier.Type == ModifierType.DamageAdd ? modifier.Value : 0);

        int simpleDamage = grid.destroyed.Sum(type => type.Key != GemType.Mana ? type.Value : 0) * baseDamage;
        
        return (int) (simpleDamage * mulDamage + addDamage);
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