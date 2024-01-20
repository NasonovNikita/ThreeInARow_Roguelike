using Battle.Modifiers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "StrongerKick", menuName = "Spells/StrongerKick")]
    public class StrongerKick : Spell
    {
        public override void Cast()
        {
            if (CantCast()) return;
        
            manager.player.mana.Waste(useCost);
            LogUsage();
            manager.player.AddDamageMod(new Modifier(count, ModType.Mul, ModClass.DamageBase, value: value));
        }

        public override string Title => "Stronger Kick";

        public override string Description => $"Deal {Other.Tools.Percents(value)}% more dmg this turn";
    }
}