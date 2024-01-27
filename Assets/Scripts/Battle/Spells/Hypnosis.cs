using Battle.Modifiers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Hypnosis", menuName = "Spells/Hypnosis")]
    public class Hypnosis : Spell
    {
        public override void Cast()
        {
            if (CantCast()) return;

            manager.player.mana.Waste(useCost);
            LogUsage();
            manager.target.AddMod(new Modifier(count, ModType.Blind));
        }

        public override string Title => titleKeyRef.Value;

        public override string Description => descriptionKeyRef.Value;
    }
}