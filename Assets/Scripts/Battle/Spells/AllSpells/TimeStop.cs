public class TimeStop : Spell
{
    protected override int ManaCost => 80;
    protected override int Moves => 1;

    public override void Cast()
    {
        if (manager.State != BattleState.PlayerTurn) return;
        
        player.ChangeMana(-ManaCost);
        player.target.StatusModifiers.Add(new Modifier(Moves, ModifierType.Stun));
    }
}