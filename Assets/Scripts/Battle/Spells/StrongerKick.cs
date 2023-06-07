using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "StrongerKick", menuName = "Spells/StrongerKick")]
    public class StrongerKick : Spell
    {
        public override void Cast()
        {
            if (CantCast()) return;
        
            manager.player.mana -= manaCost;
            manager.player.damage.AddMod(new Modifier(moves, ModType.Mul, value), FuncAffect.Get);
        }
    }
}