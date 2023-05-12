using System;
using System.Linq;

[Serializable]
public class Player : Unit
{
    public int manaPerGem;

    public Grid grid;

    private int CountMana()
    {
        return grid.destroyed.ContainsKey(GemType.Mana) ? grid.destroyed[GemType.Mana] * manaPerGem : 0;
    }

    private int CountDamage()
    {
        return (int) (grid.destroyed.Sum(type => type.Key != GemType.Mana ? type.Value : 0) * damage.GetValue());
    }

    public override void Act()
    {
        mana += CountMana();
        AudioManager.instance.Play(AudioEnum.EnemyHit);
        manager.target.DoDamage(CountDamage());
    }

    protected override void NoHp()
    {
        StartCoroutine(manager.Die());
    }
}