using System.Collections.Generic;
using System.Linq;
using Battle.Match3;
using Battle.Modifiers;
using Other;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "MoreDmgRandEnemy", menuName = "Spells/MoreDmgRandEnemy")]
    public class MoreDmgRandEnemy : Spell
    {
        protected override void Action()
        {
            var possible = manager.enemies.Where(v => v != null)
                .Where(enemy => !enemy.damage.GetGemsDamage(
                    new Dictionary<GemType, int>
                    {
                        { GemType.Blue, 1 },
                        { GemType.Green, 1 },
                        { GemType.Red, 1 },
                        { GemType.Yellow, 1 }
                    }).IsZero)
                .ToList();
            
            Tools.Random.RandomChoose(possible).AddDamageMod(new Modifier(count, ModType.Mul,
                ModClass.DamageBase, value: value, delay: true));
        }
    }
}