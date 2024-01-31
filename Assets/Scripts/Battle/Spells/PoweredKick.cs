using Battle.Modifiers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "PoweredKick", menuName = "Spells/PoweredKick")]
    public class PoweredKick : Spell
    {
        public override void Cast()
        {
            if (CantCastOrCast()) return;
        
            manager.player.AddMod(new Modifier(count, ModType.Stun));
            manager.player.AddDamageMod(new Modifier(count + 1, ModType.Mul, 
                ModClass.DamageBase, value: value));
            manager.EndTurn();
        }

        public override string Title => titleKeyRef.Value;

        public override string Description => string.Format(descriptionKeyRef.Value, count, 1 + value);
    }
}