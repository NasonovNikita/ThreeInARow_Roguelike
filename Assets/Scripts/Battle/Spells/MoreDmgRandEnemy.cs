using System.Linq;
using Battle.Modifiers.StatModifiers;
using Other;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "MoreDmgRandEnemy", menuName = "Spells/MoreDmgRandEnemy")]
    public class MoreDmgRandEnemy : Spell
    {
        [SerializeField] private int damage;
        [SerializeField] private int moves;
        protected override void Action()
        {
            var possible = unitBelong.Enemies
                .Where(enemy => enemy != null && enemy.damage != 0)
                .ToList();
            
            Tools.Random.RandomChoose(possible).damage.mods.Add(new DamageMoveMod(damage, moves));
        }
    }
}