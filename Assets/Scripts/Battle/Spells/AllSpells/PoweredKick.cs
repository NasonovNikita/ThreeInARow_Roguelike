public class PoweredKick : Spell
{
    protected override int ManaCost => 45;
    
    protected override int Moves => 1;

    private const float Value = 2.0f;

    public override void Cast()
    {
        if (CanCast()) return;
        
        Player.mana -= ManaCost;
        Player.statusModifiers.Add(new Modifier(Moves, ModifierType.Stun, Player.statusModifiers,() => true));
        Player.damage.onGetMods.Add(new Modifier(Moves,ModifierType.Mul, Player.damage.onGetMods, () => true, Value));
        Manager.EndTurn();
    }
}