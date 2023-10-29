using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "UnitData/EnemyData")]
public class EnemyData : UnitData
{
    public EnemyData(Stat hp, Stat mana, Stat fDmg, Stat cDmg, Stat pDmg, Stat lDmg, Stat phDmg, List<Modifier> statusModifiers, List<Item> items,
        List<Spell> spells) : base(hp, mana, fDmg, cDmg, pDmg, lDmg, phDmg, statusModifiers, items, spells) {}
    
    public EnemyData() : base() {}

    public Enemy Init() 
    {
        Enemy enemy = Instantiate(PrefabsContainer.instance.enemy);
        base.Init(enemy);

        return enemy;
    }
}