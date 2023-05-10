using System;

[Serializable]
public class Enemy : Unit
{
    [NonSerialized]
    public Player player;

    public override void Act()
    {
        if (Stunned() || manager.State == BattleState.End) return;
        
        player.DoDamage((int) damage.GetValue());
    }

    protected override void NoHp()
    {
        StartCoroutine(manager.KillEnemy(this));
    }
}