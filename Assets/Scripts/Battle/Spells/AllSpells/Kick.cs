public class Kick : Spell
{
    protected override int ManaCost => 50;
    protected override int Moves => 1;

    private const int Value = 100;

    public override void Cast()
    {
        if (manager.State != BattleState.PlayerTurn || player.Mana < ManaCost) return;
        
        player.ChangeMana(-ManaCost);
        player.target.ChangeHp(-Value);
    }
}