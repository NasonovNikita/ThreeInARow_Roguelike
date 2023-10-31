using Battle;
using UnityEngine;

[CreateAssetMenu(fileName = "Singe", menuName = "Spells/Singe")]
public class Singe : Spell
{
    public override void Cast()
    {
        attachedUnit.mana -= manaCost;
        Damage dmg = new Damage(fDmg: 15);
        manager.player.DoDamage(dmg);
        //TODO Apply burning
    }
}