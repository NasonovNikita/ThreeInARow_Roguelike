using System.Linq;
using Battle.Modifiers;
using Battle.Units;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "TimeStop", menuName = "Spells/TimeStop")]
    public class TimeStop : Spell
    {
        protected override void Action()
        {
            foreach (Enemy enemy in manager.enemies.Where(v => v != null))
            {
                enemy.AddMod(new Modifier(count, ModType.Stun));
            }
        }

        public override string Description => descriptionKeyRef.Value;
    }
}