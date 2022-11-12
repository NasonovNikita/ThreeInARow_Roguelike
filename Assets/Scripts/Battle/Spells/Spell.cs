using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    protected abstract int ManaCost { get;}
    protected Unit Unit;
    protected BattleManager Manager;
    
    public virtual void Cast()
    {
        Debug.Log("Spell is casting");
    }
}
