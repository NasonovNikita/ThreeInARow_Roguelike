using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
[Serializable]
public class Item : ScriptableObject
{
    [SerializeField] private UnitStat stat;
    [SerializeField] private ModAffect affects;
    [SerializeField] private Modifier mod;
    
    public void Use(Unit unitBelong)
    {
        Stat checkStat = stat switch
        {
            UnitStat.Hp => unitBelong.hp,
            UnitStat.Mana => unitBelong.mana,
            UnitStat.Damage => unitBelong.damage,
            _ => throw new ArgumentOutOfRangeException()
        };
        checkStat.AddMod(mod, affects);
    }
}