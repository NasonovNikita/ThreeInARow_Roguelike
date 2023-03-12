public class PoweredKick : Spell
{
    protected override int ManaCost => 45;
    
    protected override int Moves => 1;

    private const float Value = 2.0f;

    public override void Cast()
    {
        if (CanCast()) return;
        
        player.mana -= ManaCost;
        player.StatusModifiers.Add(new Modifier(Moves, ModifierType.Stun, player.StatusModifiers));
        player.damage.onGetMods.Add(new Modifier(Moves,ModifierType.Mul, player.damage.onGetMods, Value));
        manager.EndTurn();
    }
}