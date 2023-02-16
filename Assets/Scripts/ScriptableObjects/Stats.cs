using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Stats", order = 52)]
public class Stats : ScriptableObject
{
    public int playerBaseHp;
    public int playerHp;
    public int playerBaseMana;
    public int playerMana;
    public int playerBaseDamage;
    public int manaPerGem;

    public void Reset()
    {
        playerHp = playerBaseHp;
        playerMana = playerBaseMana;
    }
}