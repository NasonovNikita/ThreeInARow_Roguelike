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
        public override void Cast()
        {
            if (CantCast()) return;

            unitBelong.mana.Waste(useCost);
            LogUsage();
            var possible = manager.enemies.Where(v => v != null).Where(enemy =>
                    !enemy.unitDamage.GetGemsDamage(new Dictionary<GemType, int>
                        { { GemType.Blue, 1 }, { GemType.Green, 1 }, { GemType.Red, 1 }, { GemType.Yellow, 1 } })
                    .IsZero())
                .ToList();
            Tools.RandomChoose(possible).AddDamageMod(new Modifier(count, ModType.Add,
                ModClass.DamageBase, value: value, delay: true));
        }
    }
}