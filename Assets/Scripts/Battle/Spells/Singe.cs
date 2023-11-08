using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Singe", menuName = "Spells/Singe")]
    public class Singe : Spell
    {
        public override void Cast()
        {
            attachedUnit.mana -= manaCost;
            Damage dmg = new Damage(fDmg: (int) value);
            manager.player.DoDamage(dmg);
            manager.player.StartBurning(1);
        }
    }
}