using System.Linq;
using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "FlashOfLight", menuName = "Spells/FlashOfLight")]
    public class FlashOfLight : Spell
    {
        protected override void Action()
        {
            foreach (var enemy in manager.Enemies.Where(v => v != null))
            {
                enemy.AddMod(new MoveStatModifier(count, ModType.Blind));
            }
        }

        public override string Description => descriptionKeyRef.Value;
    }
}