using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "Hypnosis", menuName = "Spells/Hypnosis")]
    public class Hypnosis : Spell
    {
        protected override void Action() => manager.target.AddMod(new MoveStatModifier(count, ModType.Blind));

        public override string Description => descriptionKeyRef.Value;
    }
}