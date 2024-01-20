using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Kick", menuName = "Spells/Kick")]
    public class Kick : Spell
    {
        public override void Cast()
        {
            if (CantCast()) return;
        
            manager.player.mana.Waste(useCost);
            LogUsage();
            PToEDamageLog.Log(manager.target, manager.player, new Damage(mDmg: (int) value));
            manager.target.DoDamage(new Damage(mDmg: (int) value));
        }

        public override string Title => "Kick";

        public override string Description => $"Deal {value} dmg";
    }
}