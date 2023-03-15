public class Enemy : Unit
{
    public Player player;

    public override void Act()
    {
        if (Stunned() || BattleManager.State == BattleState.End) return;
        
        player.DoDamage((int) damage.GetValue());
    }

    protected override void NoHp()
    {
        StartCoroutine(manager.KillEnemy(this));
    }
}