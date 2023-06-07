using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Kick", menuName = "Spells/Kick")]
    public class Kick : Spell
    {
        public override void Cast()
        {
            if (CantCast()) return;
        
            manager.player.mana -= manaCost;
            PToEDamageLog.Log(manager.target, manager.player, (int) value);
            manager.target.DoDamage((int) value);
        }
    }
}