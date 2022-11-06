using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    private int hp;
    public int Hp => hp;
    
    [SerializeField]
    private int baseHp;
    public int BaseHp => baseHp;

    [SerializeField]
    private int mana;
    public int Mana => mana;
    
    [SerializeField]
    private int basemana;
    public int Basemana => basemana;

    [SerializeField]
    private int baseDamage;
    public int BaseDamage => baseDamage;

    public void ChangeHp(int change)
    {
        hp += change;
        if (hp < 0)
        {
            hp = 0;
        }
        else if (hp > baseHp)
        {
            hp = baseHp;
        }
    }

    public void ChangeMana(int change)
    {
        mana += change;
        if (mana < 0)
        {
            mana = 0;
        }
        else if (mana > basemana)
        {
            mana = basemana;
        }
    }
}