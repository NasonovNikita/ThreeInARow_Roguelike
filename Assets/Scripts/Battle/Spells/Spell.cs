using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    protected abstract int ManaCost { get;}
    
    protected abstract int Moves { get;}

    public Player player;
    public BattleManager manager;

    public abstract void Cast();

    protected bool CanCast()
    {
        return BattleManager.State != BattleState.Turn || player.mana < ManaCost;
    }
}