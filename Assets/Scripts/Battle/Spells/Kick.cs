using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Kick", menuName = "Spells/Kick")]
    public class Kick : Spell
    {
        public override void Cast()
        {
            if (CantCastOrCast()) return;
        
            PToEDamageLog.Log(manager.target, manager.player, new Damage(mDmg: (int) value));
            manager.target.DoDamage(new Damage(mDmg: (int) value));
        }

        public override string Title => titleKeyRef.Value;

        public override string Description => string.Format(descriptionKeyRef.Value, value);
    }
}