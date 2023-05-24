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
        switch (stat)
        {
            case UnitStat.Hp:
                unitBelong.hp.AddMod(mod, affects);
                break;
            case UnitStat.Mana:
                unitBelong.mana.AddMod(mod, affects);
                break;
            case UnitStat.Damage:
                unitBelong.damage.AddMod(mod, affects);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}