using System.Linq;
using Battle.Units;
using Battle.Units.Modifiers;
using Battle.Units.Modifiers.Statuses;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "TimeStop", menuName = "Spells/TimeStop")]
    public class TimeStop : Spell
    {
        [SerializeField] private int moves;
        
        protected override void Action()
        {
            foreach (Enemy enemy in manager.Enemies.Where(v => v != null))
                IModifier.AddModToList(enemy.Statuses, new Stun(moves));
        }
    }
}