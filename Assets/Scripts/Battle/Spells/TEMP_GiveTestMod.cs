using Battle.Modifiers.StatModifiers;
using Battle.Modifiers.Statuses;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(menuName = "Spells/TEMP_GiveTestMod")]
    public class TempGiveTestMod : Spell
    {
        protected override void Action()
        {
            unitBelong.hp.onTakingDamageMods.Add(new Shield(1));
        }
    }
}