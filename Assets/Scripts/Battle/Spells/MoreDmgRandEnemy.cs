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
            if (CantCastOrCast()) return;

            var possible = manager.enemies.Where(v => v != null || v.hp != 0)
                .Where(enemy => !enemy.unitDamage.GetGemsDamage(new Dictionary<GemType, int>
                        { { GemType.Blue, 1 }, { GemType.Green, 1 }, { GemType.Red, 1 }, { GemType.Yellow, 1 } }).IsZero)
                .ToList();
            Tools.Random.RandomChoose(possible).AddDamageMod(new Modifier(count, ModType.Mul,
                ModClass.DamageBase, value: value, delay: true));
        }

        public override string Title => throw new System.NotImplementedException();

        public override string Description => throw new System.NotImplementedException();
    }
}