public class PoweredKick : Spell
{
    protected override int ManaCost => 45;
    
    private int Moves => 1;

    private const float Value = 2.0f;

    public override void Cast()
    {
        if (CantCast()) return;
        
        manager.player.mana -= ManaCost;
        manager.player.statusModifiers.Add(new Modifier(Moves, ModType.Stun, () => true));
        manager.player.damage.AddMod(new Modifier(Moves + 1, ModType.Mul,() => true, Value), ModAffect.Get);
        manager.EndTurn();
    }
}