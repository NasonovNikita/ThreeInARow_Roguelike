using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField]
    public Stat hp;
    [SerializeField]
    public Stat mana;

    public BattleManager manager;

    public List<Modifier> StatusModifiers = new();
    [SerializeField]
    public Stat damage;

    public void DoDamage(int value)
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
        return StatusModifiers.Exists(mod => mod.Type == ModifierType.Stun);
    }

    public abstract void Act();

    protected abstract void NoHp();
}