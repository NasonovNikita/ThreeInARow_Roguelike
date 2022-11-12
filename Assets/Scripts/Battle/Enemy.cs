public class Enemy : Unit
{
    public int Damage()
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

    private void NoHp()
    {
        StartCoroutine(manager.KillEnemy(this));
    }
}