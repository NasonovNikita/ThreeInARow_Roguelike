public class PoweredKick : Spell
{
    protected override int ManaCost => 45;
    
    private int Moves => 1;

    private const float Value = 2.0f;

    public override void Cast()
    {
        if (CantCast()) return;
        
        manager.player.mana -= ManaCost;
        manager.player.statusModifiers.Add(new Modifier(Moves, ModifierType.Stun, manager.player.statusModifiers,() => true));
        manager.player.damage.onGetMods.Add(new Modifier(Moves, ModifierType.Mul, manager.player.damage.onGetMods, () => true, Value));
        manager.EndTurn();
    }
}