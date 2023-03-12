public class Enemy : Unit
{
    public Player player;

    public override void Act()
    {
        if (Stunned() || manager.State == BattleState.End) return;
        
        player.DoDamage(damage.GetValue());
    }

    protected override void NoHp()
    {
        StartCoroutine(manager.KillEnemy(this));
    }
}