using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    protected abstract int ManaCost { get;}

    public BattleManager manager;

    public void Awake()
    {
        manager = FindObjectOfType<BattleManager>();
    }

    public abstract void Cast();

    protected bool CantCast()
    {
        return manager.State != BattleState.Turn || manager.player.mana < ManaCost;
    }
}