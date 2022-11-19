using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    protected abstract int ManaCost { get;}
    
    protected abstract int Moves { get;}

    public Player player;
    public BattleManager manager;
    public Enemy target;

    public abstract void Cast();
}