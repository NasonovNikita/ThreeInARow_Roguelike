using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    protected abstract int ManaCost { get;}
    
    protected abstract int Moves { get;}

    public Player player;
    public BattleManager manager;
    public Enemy target;
    
    public virtual void Cast()
    {
        Debug.Log("Spell is casting");
    }
}