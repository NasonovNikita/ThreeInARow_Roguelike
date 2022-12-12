public class TimeStop : Spell
{
    protected override int ManaCost => 60;
    protected override int Moves => 1;

    public override void Cast()
    {
        if (manager.State != BattleState.PlayerTurn || player.Mana < ManaCost) return;
        
        player.ChangeMana(-ManaCost);
        
        foreach (Enemy enemy in player.enemies)
        {
            enemy.StatusModifiers.Add(new Modifier(Moves, ModifierType.Stun));
        }
    }
}