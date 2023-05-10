public class StrongerKick : Spell
{
    protected override int ManaCost => 25;
    private int Moves => 1;

    private const float Value = 0.3f;

    public override void Cast()
    {
        if (CantCast()) return;
        
        manager.player.mana -= ManaCost;
        manager.player.damage.onGetMods.Add(new Modifier(Moves, ModifierType.Mul, manager.player.damage.onGetMods, () => true, Value));
    }
}