using System;
using System.Collections;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "ChainLightning", menuName = "Spells/ChainLightning")]
    public class ChainLightning : Spell
    {
        [SerializeField] private float rise;
        public override IEnumerator Cast()
        {
            if (CantCastOrCast()) yield break;

            int nulls = 0;
            for (int i = 0; i < manager.enemies.Count; i++)
            {
                Damage dmg = new Damage(mDmg: (int)(value * Math.Pow(rise, i - nulls)));
                var enemy = manager.enemies[i];
                if (enemy == null)
                {
                    nulls += 1;
                    continue;
                }
                enemy.TakeDamage(dmg);
                //PToEDamageLog.Log(enemy, manager.player, dmg);
            }
            
            
            yield return Wait();
        }

        public override string Description =>
            string.Format(descriptionKeyRef.Value, (int)value, Other.Tools.Percents(rise - 1));
    }
}