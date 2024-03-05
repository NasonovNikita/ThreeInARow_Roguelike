using System.Linq;
using Battle.Modifiers;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "FlashOfLight", menuName = "Spells/FlashOfLight")]
    public class FlashOfLight : Spell
    {
        protected override void Action()
        {
            foreach (var enemy in manager.enemies.Where(v => v != null))
            {
                enemy.AddMod(new Modifier(count, ModType.Blind));
            }
        }

        public override string Description => descriptionKeyRef.Value;
    }
}