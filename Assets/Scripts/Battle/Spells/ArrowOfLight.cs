using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "ArrowOfLight", menuName = "Spells/ArrowOfLight")]
    public class ArrowOfLight : Spell
    {
        public override void Cast()
        {
            if (CantCast()) return;
            
            manager.player.mana.Waste(useCost);
            LogUsage();
            PToEDamageLog.Log(manager.target, manager.player, new Damage(lDmg: 30));
            manager.target.DoDamage(new Damage(lDmg: 30));
        }
    }
}