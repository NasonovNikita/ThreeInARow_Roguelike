using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "StrongerKick", menuName = "Spells/StrongerKick")]
    public class StrongerKick : Spell
    {
        protected override void Action()
        {
            unitBelong.AddDamageMod(new MoveStatModifier(count, ModType.Add, ModClass.Damage, value: value)); // TODO on modType change
        }

        public override string Description => string.Format(descriptionKeyRef.Value, Other.Tools.Percents(value));
    }
}