using System;
using Other;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "ChainLightning", menuName = "Spells/ChainLightning")]
    public class ChainLightning : Spell
    {
        [SerializeField] private int dmg;
        [SerializeField] private float rise;

        public override string Description =>
            string.Format(descriptionKeyRef.Value, dmg, Tools.Percents(rise - 1));

        protected override void Action()
        {
            var nulls = 0;
            for (var i = 0; i < UnitBelong.Enemies.Count; i++)
            {
                var damage = (int)(dmg * Math.Pow(rise, i - nulls));
                var enemy = UnitBelong.Enemies[i];
                if (enemy == null)
                {
                    nulls += 1;
                    continue;
                }

                enemy.TakeDamage(damage);
            }
        }
    }
}