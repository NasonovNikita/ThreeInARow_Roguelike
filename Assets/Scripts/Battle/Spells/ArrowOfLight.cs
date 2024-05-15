using System;
using UnityEngine;

namespace Battle.Spells
{
    [Obsolete]
    [CreateAssetMenu(fileName = "ArrowOfLight", menuName = "Spells/ArrowOfLight")]
    public class ArrowOfLight : Spell
    {
        [SerializeField] private int dmg;
        protected override void Action() =>
            unitBelong.target.hp.TakeDamage(dmg);

        public override string Description =>
            string.Format(descriptionKeyRef.Value, dmg);
    }
}