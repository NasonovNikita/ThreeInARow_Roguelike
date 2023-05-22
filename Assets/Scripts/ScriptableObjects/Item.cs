using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class Item : ScriptableObject
{
    [SerializeField]
    private Modifier mod;
    
    public void Use()
    {
        
    }
}