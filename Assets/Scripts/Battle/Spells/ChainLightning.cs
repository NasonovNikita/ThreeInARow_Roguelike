using System;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "ChainLightning", menuName = "Spells/ChainLightning")]
    public class ChainLightning : Spell
    {
        [SerializeField] private int dmg;
        [SerializeField] private float rise;

        protected override void Action()
        {
            int nulls = 0;
            for (int i = 0; i < unitBelong.Enemies.Count; i++)
            {
                int damage = (int)(dmg * Math.Pow(rise, i - nulls));
                var enemy = unitBelong.Enemies[i];
                if (enemy == null)
                {
                    nulls += 1;
                    continue;
                }

                enemy.hp.TakeDamage(damage);
            }
        }

        public override string Description =>
            string.Format(descriptionKeyRef.Value, dmg, Other.Tools.Percents(rise - 1));
    }
}