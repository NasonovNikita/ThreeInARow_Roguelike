using Battle.Units;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Healing", menuName = "Spells/Healing")]
    public class Healing : Spell
    {
        public bool offBattle;
        public override void Cast()
        {
            if (offBattle)
            {
                if (Player.data.mana <= useCost) return;
                Player.data.mana -= useCost;
                Player.data.hp += Player.data.hp.GetValue() * value;
            }
            else
            {
                if (CantCast()) return;
                attachedUnit.mana -= useCost;
                attachedUnit.hp += attachedUnit.hp.GetValue() * value;
            }
        }
    }
}