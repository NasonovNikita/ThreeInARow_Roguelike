using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    protected abstract int ManaCost { get;}
    
    protected abstract int Moves { get;}

    public BattleManager Manager;

    public abstract void Cast();

    protected bool CanCast()
    {
        return BattleManager.State != BattleState.Turn || BattleManager.Player.mana < ManaCost;
    }
}