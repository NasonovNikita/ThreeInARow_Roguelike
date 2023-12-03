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

            manager.player.mana -= useCost;
            manager.target.stateModifiers.Add(new Modifier(count, ModType.Blind));
        }
    }
}