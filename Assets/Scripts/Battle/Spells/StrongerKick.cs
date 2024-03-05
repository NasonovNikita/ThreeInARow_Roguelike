using Battle.Modifiers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "StrongerKick", menuName = "Spells/StrongerKick")]
    public class StrongerKick : Spell
    {
        protected override void Action()
        {
            unitBelong.AddDamageMod(new Modifier(count, ModType.Mul, ModClass.DamageBase, value: value));
        }

        public override string Description => string.Format(descriptionKeyRef.Value, Other.Tools.Percents(value));
    }
}