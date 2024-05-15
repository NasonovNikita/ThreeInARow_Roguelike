using Battle.Modifiers.StatModifiers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(menuName = "Spells/TEMP_GiveTestMod")]
    public class TempGiveTestMod : Spell
    {
        protected override void Action()
        {
            unitBelong.hp.onHealingMods.Add(new DamageMoveMod(1, 10, true));
        }
    }
}