using System;

[Serializable]
public class Enemy : Unit
{
    private Player player;

    public new void Awake()
    {
        base.Awake();

        player = FindObjectOfType<Player>();
    }
    public override void DoDamage(int value)
    {
        base.DoDamage(value);

        if (value != 0)
        {
            AudioManager.instance.Play(AudioEnum.EnemyHit);
        }
    }

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