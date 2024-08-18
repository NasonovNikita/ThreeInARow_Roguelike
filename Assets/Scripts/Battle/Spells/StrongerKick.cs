using System;
using Battle.Units.StatModifiers;
using UnityEngine;

namespace Battle.Spells
{
    [Serializable]
    [CreateAssetMenu(fileName = "StrongerKick", menuName = "Spells/StrongerKick")]
    public class StrongerKick : Spell
    {
        [SerializeField] private int damage;
        [SerializeField] private int moves;

        public override string Description =>
            string.Format(descriptionKeyRef.Value, damage, moves);

        protected override void Action()
        {
            UnitBelong.damage.mods.Add(new DamageMoveMod(damage, moves));
        }
    }
}