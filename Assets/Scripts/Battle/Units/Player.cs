using System.Linq;
using UnityEngine;

public class Player : Unit
{
    [SerializeField]
    private int manaPerGem;

    public Enemy target;

    public Grid grid;

    public void Awake()
    {
        BattleManager.Player = this;
        if (BattleManager.Grid != null)
        {
            manager.TurnOn();
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
        target.DoDamage(CountDamage());
        grid.Destroyed.Clear();
    }

    protected override void NoHp()
    {
        StartCoroutine(manager.Die());
    }
}