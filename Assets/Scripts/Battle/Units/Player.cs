using System;
using System.Linq;

[Serializable]
public class Player : Unit
{
    private int manaPerGem;

    public Grid grid;

    public void Awake()
    {
        BattleManager.Player = this;
        if (BattleManager.Grid != null)
        {
            BattleManager.TurnOn();
        }
    }

    private int CountMana()
    {
        return grid.Destroyed.ContainsKey(GemType.Mana) ? grid.Destroyed[GemType.Mana] * manaPerGem : 0;
    }

    private int CountDamage()
    {
        return (int) (grid.Destroyed.Sum(type => type.Key != GemType.Mana ? type.Value : 0) * damage.GetValue());
    }

    public override void Act()
    {
        mana += CountMana();
        BattleManager.target.DoDamage(CountDamage());
    }

    protected override void NoHp()
    {
        StartCoroutine(BattleManager.Die());
    }
}