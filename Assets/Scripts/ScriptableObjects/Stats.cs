using UnityEngine;


[CreateAssetMenu(fileName = "Stats", menuName = "Stats")]
public class Stats : ScriptableObject
{
    public Stat playerHp;
    public Stat playerMana;
    public Stat playerDamage;
    public int manaPerGem;
}