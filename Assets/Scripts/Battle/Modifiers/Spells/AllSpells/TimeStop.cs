public class TimeStop : Spell
{
    protected override int ManaCost => 50;
    protected override int Moves => 1;

    public override void Cast()
    {
        if (CanCast()) return;
        
        player.mana -= ManaCost;
        
        foreach (Enemy enemy in BattleManager.Enemies)
        {
            enemy.StatusModifiers.Add(new Modifier(Moves, ModifierType.Stun, enemy.StatusModifiers));
        }
    }
}