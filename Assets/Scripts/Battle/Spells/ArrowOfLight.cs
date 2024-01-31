using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "ArrowOfLight", menuName = "Spells/ArrowOfLight")]
    public class ArrowOfLight : Spell
    {
        public override void Cast()
        {
            if (CantCastOrCast()) return;
            PToEDamageLog.Log(manager.target, manager.player, new Damage(lDmg: (int) value));
            manager.target.DoDamage(new Damage(lDmg: (int) value));
        }

        public override string Title => titleKeyRef.Value;

        public override string Description => string.Format(descriptionKeyRef.Value, (int) value);
    }
}