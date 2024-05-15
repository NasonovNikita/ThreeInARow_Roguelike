using System;
using Battle.Modifiers.StatModifiers;
using UnityEngine;

namespace Battle.Spells
{
    [Serializable]
    [CreateAssetMenu(fileName = "StrongerKick", menuName = "Spells/StrongerKick")]
    public class StrongerKick : Spell
    {
        [SerializeField] private int damage;
        [SerializeField] private int moves;

        protected override void Action() =>
            unitBelong.damage.mods.Add(new DamageMoveMod(damage, moves));

        public override string Description =>
            string.Format(descriptionKeyRef.Value, damage, moves);
    }
}