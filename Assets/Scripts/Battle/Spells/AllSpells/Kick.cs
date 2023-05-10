public class Kick : Spell
{
    protected override int ManaCost => 50;

    private const int Value = 80;

    public override void Cast()
    {
        if (CantCast()) return;
        
        manager.player.mana -= ManaCost;
        manager.target.DoDamage(Value);
    }
}