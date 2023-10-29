using Battle;
using UnityEngine;

[CreateAssetMenu(fileName = "Kick", menuName = "Spells/Kick")]
public class Kick : Spell
{
    public override void Cast()
    {
        if (CantCast()) return;
        
        manager.player.mana -= manaCost;
        PToEDamageLog.Log(manager.target, manager.player, new Damage(0, 0, 0, 0, (int) value));
        manager.target.DoDamage(new Damage(0, 0, 0, 0, (int) value));
    }
}