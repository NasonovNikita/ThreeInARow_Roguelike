using UnityEngine;

[CreateAssetMenu(fileName = "Kick", menuName = "Spells/Kick")]
public class Kick : Spell
{
    public override void Cast()
    {
        if (CantCast()) return;
        
        manager.player.mana -= manaCost;
        manager.target.DoDamage((int) value);
    }
}