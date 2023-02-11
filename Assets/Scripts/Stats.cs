using UnityEngine;

[CreateAssetMenu(fileName = "StatsHandler", menuName = "Stats Handler", order = 52)]
public class Stats : ScriptableObject
{
    [SerializeField] private int playerBaseHp;
    [SerializeField] private int playerHp;
    [SerializeField] private int playerBaseMana;
    [SerializeField] private int playerMana;
    [SerializeField] private int playerBaseDamage;
    [SerializeField] private int playerDamage;
    [SerializeField] private int manaPerGem;
}
