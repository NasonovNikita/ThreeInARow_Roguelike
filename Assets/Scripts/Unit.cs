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
    private int baseDamage;
    public int BaseDamage => baseDamage;
    

    public void ChangeHp(int change)
    {
        hp += change;
    }
}
