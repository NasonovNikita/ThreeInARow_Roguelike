using Battle;
using UnityEngine;

public abstract class Spell : ScriptableObject
{
    [SerializeField] protected int manaCost;

    [SerializeField] public string title;

    [SerializeField] protected float value;

    [SerializeField] protected int moves;

    protected Unit attachedUnit;

    protected BattleManager manager;

    public void Init(Unit unit)
    {
        attachedUnit = unit;
        manager = FindFirstObjectByType<BattleManager>();
    }

    public abstract void Cast();

    protected bool CantCast()
    {
        return manager.State != BattleState.Turn || manager.player.mana < manaCost;
    }

    protected static void ApplyToDamage(Unit unit, Modifier mod, ModAffect affects)
    {
        unit.fDmg.AddMod(mod, affects);
        unit.cDmg.AddMod(mod, affects);
        unit.pDmg.AddMod(mod, affects);
        unit.lDmg.AddMod(mod, affects);
        unit.phDmg.AddMod(mod, affects);
    }
}