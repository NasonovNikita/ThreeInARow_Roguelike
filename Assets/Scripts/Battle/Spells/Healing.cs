using Battle.Units;
using Battle.Units.Stats;
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
                Player.data.mana.Waste(useCost);
                Player.data.unitHp = (UnitHp) (Player.data.unitHp + (int) (Player.data.unitHp.value * value));
            }
            else
            {
                if (CantCast()) return;
                attachedUnit.mana.Waste(useCost);
                attachedUnit.unitHp = (UnitHp) (attachedUnit.unitHp + (int) (attachedUnit.unitHp.value * value));
            }
        }
    }
}