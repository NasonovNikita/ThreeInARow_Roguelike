using Battle.Units;
using Battle.Units.Statuses;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "TimeStop", menuName = "Spells/TimeStop")]
    public class TimeStop : Spell
    {
        [SerializeField] private int moves;

        protected override void Action()
        {
            foreach (Unit enemy in unitBelong.Enemies) enemy.Statuses.Add(new Stun(moves));
        }
    }
}