using System;

[Serializable]
public class Enemy : Unit
{
    [NonSerialized]
    public Player player;

    public override void Act()
    {
        if (Stunned() || BattleManager.State == BattleState.End) return;
        
        player.DoDamage((int) damage.GetValue());
    }

    protected override void NoHp()
    {
        StartCoroutine(BattleManager.KillEnemy(this));
    }
}