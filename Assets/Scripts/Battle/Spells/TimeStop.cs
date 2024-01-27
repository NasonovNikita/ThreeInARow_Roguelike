using System.Linq;
using Battle.Modifiers;
using Battle.Units;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "TimeStop", menuName = "Spells/TimeStop")]
    public class TimeStop : Spell
    {
        public override void Cast()
        {
            if (CantCast()) return;
        
            manager.player.mana.Waste(useCost);
            LogUsage();
        
            foreach (Enemy enemy in manager.enemies.Where(v => v != null))
            {
                enemy.AddMod(new Modifier(count, ModType.Stun));
            }
        }

        public override string Title => titleKeyRef.Value;

        public override string Description => descriptionKeyRef.Value;
    }
}