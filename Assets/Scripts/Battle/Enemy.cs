public class Enemy : Unit
{
    public Player player;

    private int Damage()
    {
        return baseDamage;
    }

    public override void Act()
    {
        player.ChangeHp(-Damage());
        Move();
    }

    protected override void NoHp()
    {
        StartCoroutine(manager.KillEnemy(this));
    }
}