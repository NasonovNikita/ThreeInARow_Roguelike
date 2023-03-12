using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats", order = 52)]
public class Stats : ScriptableObject
{
    public Stat playerHp;
    public Stat playerMana;
    public Stat playerDamage;
    public int manaPerGem;

    public void Reset()
    {
        playerHp = new Stat(playerHp);
        playerMana = new Stat(playerMana);
        playerDamage = new Stat(playerDamage);
    }
}