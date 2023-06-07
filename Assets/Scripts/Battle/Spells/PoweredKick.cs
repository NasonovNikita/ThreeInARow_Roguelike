using UnityEngine;

namespace Battle.Spells
{
    [CreateAssetMenu(fileName = "PoweredKick", menuName = "Spells/PoweredKick")]
    public class PoweredKick : Spell
    {
        public override void Cast()
        {
            if (CantCast()) return;
        
            manager.player.mana -= manaCost;
            manager.player.statusModifiers.Add(new Modifier(moves, ModType.Stun));
            manager.player.damage.AddMod(new Modifier(moves + 1, ModType.Mul, value), FuncAffect.Get);
            manager.EndTurn();
        }
    }
}