using System.Collections.Generic;
using Battle.Units.Enemies;
using UnityEngine;

namespace Battle.Units.Data
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "UnitData/EnemyData")]
    public class EnemyData : UnitData
    {
        public EnemyData(Stat hp, Stat mana, Stat damage, List<Modifier> statusModifiers, List<Item> items,
            List<Spell> spells) : base(hp, mana, damage, statusModifiers, items, spells) {}
    
        public EnemyData() : base() {}

        public Enemy Init()
        {
            Enemy enemy = Instantiate(PrefabsContainer.instance.enemy);
            base.Init(enemy);

            return enemy;
        }
    }
}