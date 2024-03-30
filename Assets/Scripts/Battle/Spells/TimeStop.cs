using Battle.Modifiers.Statuses;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "TimeStop", menuName = "Spells/TimeStop")]
    public class TimeStop : Spell
    {
        [SerializeField] private int moves;
        
        protected override void Action()
        {
            foreach (var enemy in unitBelong.Enemies)
            {
                enemy.AddStatus(new Stun(moves));
            }
        }
    }
}