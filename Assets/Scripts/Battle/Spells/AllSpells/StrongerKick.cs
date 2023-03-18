public class StrongerKick : Spell
{
    protected override int ManaCost => 25;
    protected override int Moves => 1;

    private const float Value = 0.3f;

    public override void Cast()
    {
        if (CanCast()) return;
        
        Player.mana -= ManaCost;
        Player.damage.onGetMods.Add(new Modifier(Moves, ModifierType.Mul, Player.damage.onGetMods, () => true, Value));
    }
}