using Battle.Modifiers;
using Battle.Units.Enemies;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "TimeStop", menuName = "Spells/TimeStop")]
    public class TimeStop : Spell
    {
        public override void Cast()
        {
            if (CantCast()) return;
        
            manager.player.mana -= useCost;
        
            foreach (Enemy enemy in manager.enemies)
            {
                enemy.stateModifiers.Add(new Modifier(count, ModType.Stun));
            }
        }
    }
}