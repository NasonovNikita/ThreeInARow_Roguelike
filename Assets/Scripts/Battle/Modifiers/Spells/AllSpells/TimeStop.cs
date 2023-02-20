public class TimeStop : Spell
{
    protected override int ManaCost => 50;
    protected override int Moves => 1;

    public override void Cast()
    {
        if (CanCast()) return;
        
        player.ChangeMana(-ManaCost);
        
        foreach (Enemy enemy in player.enemies)
        {
            enemy.StatusModifiers.Add(new Modifier(Moves, ModifierType.Stun));
        }
    }
}