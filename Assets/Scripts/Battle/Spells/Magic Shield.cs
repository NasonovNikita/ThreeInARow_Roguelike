using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "MagicShield", menuName = "Spells/MagicShield")]
    public class MagicShield : Spell
    {
        public override void Cast()
        {
            if (CantCast()) return;
        
            manager.player.mana -= manaCost;
            manager.player.hp.AddMod(new Modifier(moves, ModType.Mul), FuncAffect.Sub);
        }
    }
}