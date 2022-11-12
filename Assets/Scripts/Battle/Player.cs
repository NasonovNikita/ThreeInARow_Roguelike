using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : Unit
{
    [SerializeField]
    private int manaPerGem;
    public int Damage(Dictionary<GemType, int> destroyed)
    {
        return destroyed.Sum(type => type.Key != GemType.Mana ? type.Value : 0) * baseDamage;
    }

    public int CountMana(Dictionary<GemType, int> destroyed)
    {
        return destroyed.ContainsKey(GemType.Mana) ? destroyed[GemType.Mana] * manaPerGem : 0;
    }
}