public class Enemy : Unit
{
    public Player player;

    private int Damage()
    {
        return baseDamage;
    }

    public new void ChangeHp(int change)
    {
        hp += change;
        if (hp <= 0)
        {
            hp = 0;
            NoHp();
        }
        else if (hp > baseHp)
        {
            hp = baseHp;
        }
    }

    public override void Act()
    {
        player.ChangeHp(-Damage());
    }

    private void NoHp()
    {
        StartCoroutine(manager.KillEnemy(this));
    }
}