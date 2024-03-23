using System.Collections.Generic;
using System.Linq;
using Battle.Match3;
using Other;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "MoreDmgRandEnemy", menuName = "Spells/MoreDmgRandEnemy")]
    public class MoreDmgRandEnemy : Spell
    {
        protected override void Action()
        {
            var possible = manager.Enemies.Where(v => v != null)
                .Where(enemy => !enemy.damage.GetGemsDamage(
                    new Dictionary<CellID, int>
                    {
                        { CellID.Blue, 1 },
                        { CellID.Green, 1 },
                        { CellID.Red, 1 },
                        { CellID.Yellow, 1 }
                    }).IsZero)
                .ToList();
            
            Tools.Random.RandomChoose(possible).AddDamageMod(new MoveStatModifier(count, ModType.Add, // TODO on modType change
                ModClass.Damage, value: value, delay: true));
        }
    }
}