using System.Linq;
using UnityEngine;

public class Player : Unit
{
    [SerializeField]
    private int manaPerGem;

    public Enemy target;

    public Grid grid;

    private int CountMana()
    {
        return grid.destroyed.ContainsKey(GemType.Mana) ? grid.destroyed[GemType.Mana] * manaPerGem : 0;
    }

    private int CountDamage()
    {
        return grid.destroyed.Sum(type => type.Key != GemType.Mana ? type.Value : 0) * damage.GetValue();
    }

    public override void Act()
    {
        mana += CountMana();
        target.DoDamage(CountDamage());
        grid.destroyed.Clear();
    }

    protected override void NoHp()
    {
        StartCoroutine(manager.Die());
    }
}