using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    protected abstract int ManaCost { get;}
    protected abstract int Moves { get; set; }
    [SerializeField]
    protected Unit user;
    [SerializeField]
    protected BattleManager manager;
    [SerializeField]
    protected Unit target;
    
    public virtual void Cast()
    {
        if (true)
        {
            
        }
        Debug.Log("Spell is casting");
    }
}