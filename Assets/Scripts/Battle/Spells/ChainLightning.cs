using System;
using Battle.Units;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "ChainLightning", menuName = "Spells/ChainLightning")]
    public class ChainLightning : Spell
    {
        [SerializeField] private float rise;
        public override void Cast()
        {
            if (CantCast()) return;

            manager.player.mana.Waste(useCost);
            for (int i = 0; i < manager.enemies.Count; i++)
            {
                Damage dmg = new Damage(mDmg: (int)(value * Math.Pow(rise, i)));
                Enemy enemy = manager.enemies[i];
                enemy.DoDamage(dmg);
                PToEDamageLog.Log(enemy, manager.player, dmg);
            }
        }
    }
}