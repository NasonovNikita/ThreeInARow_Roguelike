public class TimeStop : Spell
{
    protected override int ManaCost => 50;
    private int Moves => 1;

    public override void Cast()
    {
        if (CantCast()) return;
        
        manager.player.mana -= ManaCost;
        
        foreach (Enemy enemy in BattleManager.enemies)
        {
            enemy.statusModifiers.Add(new Modifier(Moves, ModifierType.Stun, enemy.statusModifiers, () => true));
        }
    }
}